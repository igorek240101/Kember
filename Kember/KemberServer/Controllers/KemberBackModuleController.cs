using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Reflection;
using KemberInterface;

namespace Kember
{
    [ApiController]
    [Route("KemberBackModule")]
    public class KemberBackModuleController : ControllerBase
    {
        [Route("Registration")]
        [HttpPost]
        public ActionResult Registration(string name, string key)
        {
            try
            {
                 KemberBackModule.Registration(name, key);

            }
            catch
            {
                return UnprocessableEntity();
            }
            return Ok();
        }



        [Route("Login/{name}")]
        [HttpGet]
        public ActionResult Login(string name)
        {
            try
            {
                return Ok(KemberBackModule.Login(name));
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
            try
            {
                return Ok(KemberBackModule.Load(key, log));
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.GetType().Name);
            }
        }

        [Route("Save/{key}")]
        [HttpGet]
        public ActionResult Save(string key)
        {
            try
            {
                KemberBackModule.Save(key);
                return Ok();
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.GetType().Name);
            }
        }

        [Route("Invoke")]
        [HttpPut]
        public ActionResult Invoke(InvokeArgs args)
        {
            try
            {
                return Ok(KemberBackModule.Invoke(args.assembly, args.args, args.metric));
            }
            catch (Exception e)
            {
                return UnprocessableEntity(e.GetType().Name);
            }
        }
    }
}
