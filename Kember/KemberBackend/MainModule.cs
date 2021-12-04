using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace Kember
{
    class MainModule
    {
        static void Main(string[] args)
        {
            while(true)
            {
                switch(Console.ReadLine())
                {
                    case "Login":
                        {
                            Console.WriteLine(KemberBackModule.Login(Console.ReadLine()));
                            break;
                        }
                    case "Registration":
                        {
                            try
                            {
                                KemberBackModule.Registration(Console.ReadLine(), Console.ReadLine());
                                Console.WriteLine("True");
                            }
                            catch { Console.WriteLine("False"); }
                            break;
                        }
                    case "Load":
                        {
                            string key = Console.ReadLine();
                            string input = Console.ReadLine();
                            Metric.metric[] res = KemberBackModule.Load(key, AppDbContext.db.Logs.FirstOrDefault(t => t.Id == int.Parse(input)));
                            string s = "";
                            for (int i = 0; i < res.Length; i++)
                            {
                                if (i + 1 == res.Length)
                                {
                                    s += res[i].assembly + (char)(0) + res[i].obj;
                                }
                                else
                                {
                                    s += res[i].assembly + (char)(0) + res[i].obj + (char)(0);
                                }
                            }
                            Console.WriteLine(s);
                            break;
                        }
                    case "Save":
                        {
                            try
                            {
                                KemberBackModule.Save(Console.ReadLine());
                                Console.WriteLine("True");
                            }
                            catch { Console.WriteLine("False"); }
                            break;
                        }
                    case "Invoke":
                        {
                            Assembly[] assemblies = Console.ReadLine().Split((char)(0)).ToList().ConvertAll(t => Assembly.LoadFrom(t)).ToArray();
                            Metric.metric[] res = KemberBackModule.Invoke(assemblies, Console.ReadLine(), Console.ReadLine());
                            string s = "";
                            for(int i = 0; i < res.Length; i++)
                            {
                                if (i + 1 == res.Length)
                                {
                                    s += res[i].assembly + (char)(0) + res[i].obj;
                                }
                                else
                                {
                                    s += res[i].assembly + (char)(0) + res[i].obj + (char)(0);
                                }
                            }
                            Console.WriteLine(s);
                            break;
                        }
                    case "Loading":
                        {
                            Log[] logs = KemberBackModule.Loading(Console.ReadLine());
                            string s = "";
                            for(int i = 0; i < logs.Length; i++)
                            {
                                if (i + 1 == logs.Length)
                                {
                                    s += $"{logs[i].Id}\0{logs[i].Metric}-{logs[i].TimeMark}";
                                }
                                else
                                {
                                    s += $"{logs[i].Id}\0{logs[i].Metric}-{logs[i].TimeMark}{(char)2}";
                                }
                            }
                            Console.WriteLine(s);
                            break;
                        }
                    default: return;
                }
            }
        }
    }
}
