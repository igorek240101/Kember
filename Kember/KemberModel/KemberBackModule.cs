using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Kember
{
    public static class KemberBackModule
    {
#if DEBUG
        public static User User;
#else
        private static User User;
#endif

        private static List<(string, DateTime, string)> saves = new List<(string, DateTime, string)>();

        public static List<IMetric> metrics = new List<IMetric>();

        static KemberBackModule()
        {
            string path = new Uri(Assembly.GetAssembly(typeof(KemberBackModule)).Location).LocalPath;
            path = path.Substring(0, path.LastIndexOf("\\")) + @"\Module";
            string[] dlls = Directory.GetFiles(path, "*.dll");
            for (int i = 0; i < dlls.Length; i++)
            {
                Type[] types = null;
                Assembly assembly = Assembly.LoadFile(dlls[i]);
                try
                {
                    types = assembly.GetTypes();
                }
                catch { types = new Type[0]; }
                for (int j = 0; j < types.Length; j++)
                {
                    if (!types[j].IsAbstract && !types[j].IsEnum && types[j].IsClass && types[j].GetInterface(typeof(IMetric).Name) != null)
                    {
                        metrics.Add(types[j].GetConstructor(new Type[0]).Invoke(new object[0]) as IMetric);
                    }
                }
            }
        }

        public static object[] Invoke(Assembly[] assembly, object args, string metric)
        {
            object[] results = new object[assembly.Length];
            for (int i = 0; i < metrics.Count; i++)
            {
                if (metrics[i].GetType().Name == metric)
                {
                    for (int j = 0; j < results.Length; j++)
                    {
                        results[j] = metrics[i].RunMetric(assembly[j], args);
                    }
                    saves.Add((metric, DateTime.Now, metrics[i].Write(results, assembly)));
                    return results;
                }
            }
            return null;
        }

        public static void Save(string key)
        {
            if (!HashValidate(key)) throw new Exception();
            string name = User.Name;
            while (saves.Count > 0)
            {
                string path = "Data\\" + User.Name + saves[0].Item1 + saves[0].Item2 + ".kbr";
                AppDbContext.db.Logs.Add(new Log() { Owner = User, Metric = saves[0].Item1, TimeMark = saves[0].Item2, PathToFile = path});
                Encrypt(key, saves[0].Item3, path);
            }
        }

        public static object[] Load(string key, Log log, out Assembly[] assemblies)
        {
            if (!HashValidate(key)) throw new Exception();
            foreach(var value in metrics)
            {
                if(value.GetType().Name == log.Metric)
                {
                    return value.Read(Decrypt(key, log.PathToFile), out assemblies);
                }
            }
            throw new Exception();
        }

#if DEBUG
        public static bool HashValidate(string key)
#else
        private static bool HashValidate(string key)
#endif
        {
            if (User == null) throw new Exception();
            SHA512Managed sha = new SHA512Managed();
            string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(key)));
            return hash == User.OpenKey;
        }

#if DEBUG
        public static void Encrypt(string skey, string text, string path)
#else
        private static void Encrypt(string skey, string text, string path)
#endif
        {
            Aes aes = Aes.Create();
            byte[] key = Encoding.UTF8.GetBytes(skey);
            aes.Key = key;
            byte[] iv = aes.IV;
            FileStream file = new FileStream(path, FileMode.Create);
            file.Write(iv, 0, iv.Length);

            using (CryptoStream cryptoStream = new(file, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                using (StreamWriter encryptWriter = new(cryptoStream))
                {
                    encryptWriter.WriteLine(text);
                }
            }
        }

#if DEBUG
        public static string Decrypt(string skey, string path)
#else
        private static string Decrypt(string skey, string path)
#endif
        {
            FileStream fileStream = new(path, FileMode.Open);
            Aes aes = Aes.Create();
            byte[] iv = new byte[aes.IV.Length];
            int numBytesToRead = aes.IV.Length;
            int numBytesRead = 0;
            while (numBytesToRead > 0)
            {
                int n = fileStream.Read(iv, numBytesRead, numBytesToRead);
                if (n == 0) break;

                numBytesRead += n;
                numBytesToRead -= n;
            }
            byte[] key = Encoding.UTF8.GetBytes(skey);
            CryptoStream cryptoStream = new(fileStream, aes.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            StreamReader decryptReader = new(cryptoStream);
            return decryptReader.ReadToEnd();
        }

        public static bool Login(string name)
        {
            User user = AppDbContext.db.Users.FirstOrDefault(t => t.Name == name);
            if (user != null)
            {
                User = user;
                return true;
            }
            else return false;
        }

        public static void Registration(string name, string key)
        {
            SHA512Managed sha = new SHA512Managed();
            string hash = Encoding.UTF8.GetString(sha.ComputeHash(Encoding.UTF8.GetBytes(key)));
            User = new User() { Name = name, OpenKey = hash };
            AppDbContext.db.Users.Add(User);
            AppDbContext.db.SaveChanges();
        }
    }
}
