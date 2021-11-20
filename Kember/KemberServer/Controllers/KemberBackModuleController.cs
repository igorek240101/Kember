using KemberInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.IO;

namespace Kember
{
    [ApiController]
    [Route("KemberBackModule")]
    public class KemberBackModuleController : ControllerBase
    {
        [Route("Registration")]
        [HttpPost]
        public ActionResult Registration(Registration registration)
        {
            try
            {
                return Ok(GenerateJWT(KemberBackModule.Registration(registration.Name, registration.Key)));

            }
            catch (Exception e)
            {
                return UnprocessableEntity();
            }
        }


        [Route("Login/{name}")]
        [HttpGet]
        public ActionResult Login(string name)
        {
            try
            {
                User user = KemberBackModule.Login(name);
                if (user != null) return Ok(GenerateJWT(user));
                else return Ok("");
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.GetType().Name);
            }
        }

        [Route("Load")]
        [HttpPut]
        public ActionResult Load(string key, Log log)
        {
            int Id = isValidJWT(Request);
            if (Id != -1)
            {
                try
                {
                    return Ok(KemberBackModule.Load(key, log, AppDbContext.db.Users.FirstOrDefault(t => t.Id == Id)));
                }
                catch (Exception e)
                {
                    return UnprocessableEntity(e.GetType().Name);
                }
            }
            return Unauthorized();
        }

        [Route("Save/{key}")]
        [HttpGet]
        public ActionResult Save(string key)
        {
            int Id = isValidJWT(Request);
            if (Id != -1)
            {
                try
                {
                    KemberBackModule.Save(key, AppDbContext.db.Users.FirstOrDefault(t => t.Id == Id));
                    return Ok();
                }
                catch (Exception e)
                {
                    return UnprocessableEntity(e.GetType().Name);
                }
            }
            return Unauthorized();
        }

        [Route("Invoke")]
        [HttpPut]
        public ActionResult Invoke(InvokeArgs args)
        {
            int Id = isValidJWT(Request);
            if (Id != -1)
            {
                try
                {
                    return Ok(KemberBackModule.Invoke(args.Assembly, args.Args, args.Metric, AppDbContext.db.Users.FirstOrDefault(t => t.Id == Id)));
                }
                catch (Exception e)
                {
                    return UnprocessableEntity(e.GetType().Name);
                }
            }
            return Unauthorized();
        }


        private string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ServerInfo.Secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            var claims = new List<Claim>()
            {
                new Claim("jti",user.Id.ToString())
            };
            JwtSecurityToken token = new JwtSecurityToken(ServerInfo.Issuer, ServerInfo.Audience, claims, DateTime.Now, DateTime.MaxValue, credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static int isValidJWT(HttpRequest request)
        {
            if (request.IsHttps)
            {
                IHeaderDictionary header = request.Headers;
                string s = header["Authorization"];
                if (s != null)
                {
                    JwtSecurityTokenHandler jwt = new JwtSecurityTokenHandler();
                    if (s.Length > 7 && jwt.CanReadToken(s.Substring(7)))
                    {
                        JwtSecurityToken token = jwt.ReadJwtToken((s).Substring(7));
                        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ServerInfo.Secret));
                        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
                        var claims = new List<Claim>()
                        {
                            new Claim("jti", token.Id)
                        };
                        JwtSecurityToken validtoken = new JwtSecurityToken(token.Issuer, token.Audiences.First(), claims, token.ValidFrom, token.ValidTo, credentials);
                        validtoken = (new JwtSecurityTokenHandler()).ReadJwtToken(new JwtSecurityTokenHandler().WriteToken(validtoken));
                        if (token.Issuer == ServerInfo.Issuer && token.Audiences.First() == ServerInfo.Audience && token.RawSignature == validtoken.RawSignature && token.ValidTo < DateTime.Now)
                        {
                            return Convert.ToInt32(token.Id);
                        }
                    }
                }
            }
            return -1;
        }

        private static class ServerInfo
        {
            public static readonly string Issuer = "KemberBack";

            public static readonly string Audience = "KemberFront";

            public static readonly string Secret;

            static ServerInfo()
            {
                Secret = "MyVeryHardPasswordMyVeryHardPassword";
            }
        }
    }
}
