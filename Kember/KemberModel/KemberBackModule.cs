using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;

namespace Kember
{
    public static class KemberBackModule
    {
       public static List<IMetric> metrics = new List<IMetric>();

        static KemberBackModule()
        {
            string path = new Uri(Assembly.GetAssembly(typeof(KemberBackModule)).CodeBase).LocalPath;
            path = path.Substring(0, path.LastIndexOf("\\")) + @"\BackModule";
            string[] dlls = Directory.GetFiles(path, "*.dll");
            for(int i = 0; i < dlls.Length; i++)
            {
                Type[] types = null;
                Assembly assembly = Assembly.LoadFile(dlls[i]);
                try
                {
                    types = assembly.GetTypes();
                }
                catch { types = new Type[0]; }
                for(int j = 0; j < types.Length; j++)
                {
                    if(!types[j].IsAbstract && !types[j].IsEnum && types[j].IsClass && types[j].GetInterface(typeof(IMetric).Name) != null)
                    {
                        metrics.Add(types[j].GetConstructor(new Type[0]).Invoke(new object[0]) as IMetric);
                    }
                }
            }
        }

        public static object[] Invoke(Assembly[] assembly, object args, string metric)
        {
            object[] results = new object[assembly.Length];
            for(int i = 0; i < metrics.Count; i++)
            {
                if (metrics[i].GetType().Name == metric)
                {
                    for(int j = 0; j < results.Length; j++)
                    {
                        results[j] = metrics[i].RunMetric(assembly[j], args);
                    }
                    return results;
                }
            }
            return null;
        }

        public static void Registration(string name, string key)
        {
            SHA512Managed sha = new SHA512Managed();
            string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(key)));
            AppDbContext.db.Users.Add(new User() { Name = name, OpenKey = hash });
            AppDbContext.db.SaveChanges();
        }
    }
}
