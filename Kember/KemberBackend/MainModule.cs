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
                            KemberBackModule.Registration(Console.ReadLine(), Console.ReadLine());
                            Console.WriteLine("True");
                            break;
                        }
                    case "Load":
                        {
                            Console.WriteLine(KemberBackModule.Load(Console.ReadLine(), new Log()));
                            break;
                        }
                    case "Save":
                        {
                            KemberBackModule.Save(Console.ReadLine());
                            Console.WriteLine("True");
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
                    default: return;
                }
            }
        }
    }
}
