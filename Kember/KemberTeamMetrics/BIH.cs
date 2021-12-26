using Kember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace KemberTeamMetrics
{
    class BIH : Metric
    {
        public override metric[] RunMetric(Assembly[] assemblies, object args)
        {
            metric[] mainRes = new metric[assemblies.Length];
            for (int k = 0; k < mainRes.Length; k++)
            {
                Assembly assembly = assemblies[k];
                Flags flags = (Flags)(int.Parse(args as string));
                List<Type> types = CleanType(assembly, flags);
                bih res = new bih(types.Count);
                for (int i = 0; i < res.Length; i++)
                {
                    if (typeof(Delegate).IsAssignableFrom(types[i].BaseType))
                    {
                        res[i] = (TypeClassification(types[i]), CleanTypeName(types[i]), 0);
                    }
                    else
                    {
                        BindingFlags binding = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;
                        if ((flags & Flags.StaticMethods) != 0) binding |= BindingFlags.Static;
                        MethodInfo[] methods = types[i].GetMethods(binding);
                        binding |= BindingFlags.NonPublic;
                        MethodInfo[] privateMethods = types[i].GetMethods(binding);
                        if ((flags & Flags.StaticMethods) == 0)
                        {
                            List<MethodInfo> methodInfos = methods.ToList();
                            for (int j = 0; j < methodInfos.Count; j++)
                            {
                                if (methodInfos[j].IsStatic)
                                {
                                    methodInfos.RemoveAt(j);
                                    j--;
                                }
                            }
                            methods = methodInfos.ToArray();
                            List<MethodInfo> privateMethodInfos = privateMethods.ToList();
                            for (int j = 0; j < privateMethodInfos.Count; j++)
                            {
                                if (privateMethodInfos[j].IsStatic)
                                {
                                    privateMethodInfos.RemoveAt(j);
                                    j--;
                                }
                            }
                            privateMethods = privateMethodInfos.ToArray();
                        }
                        if (privateMethods.Length != 0)
                        {
                            res[i] = (TypeClassification(types[i]), CleanTypeName(types[i]), ((double)methods.Length) / privateMethods.Length);
                        }
                        else
                        {
                            res[i] = (TypeClassification(types[i]), CleanTypeName(types[i]), 0);
                        }
                    }
                }
                mainRes[k] = new metric(res, assembly.GetName().Name);
            }
            return mainRes;
        }

        public override metric[] Read(string input)
        {
            string[] main = input.Split('\r');
            metric[] res = new metric[main.Length];
            for (int i = 0; i < main.Length; i++)
            {
                string[] assembly = main[i].Split('\n');
                res[i].assembly = assembly[0];
                bih bihs = new bih(assembly.Length - 1);
                for (int j = 1; j < assembly.Length; j++)
                {
                    string[] bih = assembly[j].Split(' ');
                    bihs[j - 1] = (bih[0], bih[1], double.Parse(bih[2]));
                }
                res[i].obj = bihs;
            }
            return res;
        }

        public override string Write(metric[] output)
        {
            string res = "";
            for (int i = 0; i < output.Length; i++)
            {
                res += output[i].assembly + '\n';
                bih input = (bih)output[i].obj;
                for (int j = 0; j < input.Length; j++)
                {
                    res += input[j].Item1 + " " + input[j].Item2 + " " + input[j].Item3;
                    if (j + 1 != input.Length)
                    {
                        res += '\n';
                    }
                    else
                    {
                        if (i + 1 != output.Length) res += '\r';
                    }
                }
            }
            return res;
        }

        private List<Type> CleanType(Assembly assembly, Flags flags)
        {
            List<Type> types = assembly.GetTypes().ToList();
            for (int i = 0; i < types.Count; i++)
            {
                if ((flags & Flags.StaticClass) == 0 && types[i].IsAbstract && types[i].IsSealed && false) // Требуется уточнение подхода для языков отличных от C$
                {
                    types.RemoveAt(i);
                    i--; continue;
                }
                else
                {
                    if ((flags & Flags.Delegate) == 0 && typeof(Delegate).IsAssignableFrom(types[i].BaseType)) // Костыль
                    {
                        types.RemoveAt(i);
                        i--; continue;
                    }
                    else
                    {
                        if ((flags & Flags.AnonymousType) == 0 && false) // В явном виде не может быть реализовано
                        {
                            types.RemoveAt(i);
                            i--; continue;
                        }
                        else
                        {
                            if ((flags & Flags.Struct) == 0 && types[i].IsValueType && !types[i].IsEnum)
                            {
                                types.RemoveAt(i);
                                i--; continue;
                            }
                            else
                            {
                                if ((flags & Flags.Nested) == 0 && types[i].IsNested)
                                {
                                    types.RemoveAt(i);
                                    i--; continue;
                                }
                                else
                                {
                                    if ((flags & Flags.Enum) == 0 && types[i].IsEnum)
                                    {
                                        types.RemoveAt(i);
                                        i--; continue;
                                    }
                                    else
                                    {
                                        if ((flags & Flags.Interface) == 0 && types[i].IsInterface)
                                        {
                                            types.RemoveAt(i);
                                            i--; continue;
                                        }
                                        else
                                        {
                                            object[] array = types[i].GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);
                                            if (array != null && array.Length > 0)
                                            {
                                                types.RemoveAt(i);
                                                i--; continue;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return types;
        }

        private enum Flags
        {
            StaticClass = 0b00000000001, // NotImplementation
            Delegate = 0b00000000010,
            AnonymousType = 0b00000000100, // NotImplementation
            Struct = 0b00000001000,
            Nested = 0b00000010000,
            Enum = 0b00000100000,
            Interface = 0b00001000000,
            StaticMethods = 0b00010000000,
            Property = 0b00100000000, // NotImplementation
            RegisterAccsessors = 0b01000000000 // NotImplementation
        }

        private struct bih
        {

            (string, string, double)[] array;

            public bih(int len)
            {
                array = new (string, string, double)[len];
            }

            public int Length { get => array.Length; }

            public (string, string, double) this[int index]
            {
                get
                {
                    return array[index];
                }
                set
                {
                    array[index] = value;
                }
            }

            public override string ToString()
            {
                string s = "";
                for (int i = 0; i < array.Length; i++)
                {
                    if (i + 1 == array.Length)
                    {
                        s += $"{array[i].Item1} {array[i].Item2} - {array[i].Item3}";
                    }
                    else
                    {
                        s += $"{array[i].Item1} {array[i].Item2} - {array[i].Item3}" + ((char)0);
                    }
                }
                return s;
            }
        }
    }
}
