using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Kember;

namespace KemberTeamMetrics
{
    public class WMC : IMetric
    {

        public object RunMetric(Assembly assembly, object args)
        {
            Flags flags;
            try
            {
                flags = (Flags)(int.Parse(args as string));
            }
            catch (InvalidOperationException e) { throw new Exception("А вот нет", e); }
            List<Type> types = CleanType(assembly, flags);
            wmc res = new wmc(types.Count);
            for (int i = 0; i < res.Length; i++)
            {
                if (typeof(Delegate).IsAssignableFrom(types[i].BaseType))
                {
                    res[i] = (TypeClassification(types[i]), CleanTypeName(types[i]), 0);
                }
                else
                {
                    BindingFlags binding = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;
                    if ((flags & Flags.PrivateMethods) != 0) binding |= BindingFlags.NonPublic;
                    if ((flags & Flags.StaticMethods) != 0) binding |= BindingFlags.Static;
                    int plas = 0;
                    if ((flags & Flags.Property) != 0)
                    {
                        PropertyInfo[] properties = types[i].GetProperties(binding);
                        if ((flags & Flags.RegisterAccsessors) == 0)
                        {
                            HashSet<string> hash = new HashSet<string>();
                            foreach(var value in properties)
                            {
                                hash.Add(value.Name.Substring(value.Name.IndexOf('_')));
                            }
                            plas = hash.Count;
                        }
                        else
                        {
                            plas = properties.Length;
                        }
                    }
                    MethodInfo[] methods = types[i].GetMethods(binding);
                    if ((flags & Flags.StaticMethods) == 0)
                    {
                        List<MethodInfo> methodInfos = methods.ToList();
                        for(int j = 0; j < methodInfos.Count; j++)
                        {
                            if(methodInfos[j].IsStatic)
                            {
                                methodInfos.RemoveAt(j);
                                j--;
                            }
                        }
                        methods = methodInfos.ToArray();
                    }
                    res[i] = (TypeClassification(types[i]), CleanTypeName(types[i]), methods.Length + plas);    
                }
            }
            return res;
        }

        public (object, string)[] Read(string input)
        {
            string[] main = input.Split('\r');
            (object, string)[] res = new (object, string)[main.Length];
            for (int i = 0; i < main.Length; i++)
            {
                string[] assembly = main[i].Split('\n');
                res[i].Item2 = assembly[0];
                wmc wmcs = new wmc(assembly.Length-1);
                for (int j = 1; j < main.Length; j++)
                {
                    string[] wmc = main[j].Split(' ');
                    wmcs[j - 1] = (wmc[0], wmc[1], int.Parse(wmc[2]));
                }
            }
            return res;
        }

        public string Write((object, string)[] output)
        {
            string res = "";
            for (int i = 0; i < output.Length; i++)
            {
                res += output[i].Item2 + '\n';
                wmc input = (wmc)output[i].Item1;
                for(int j = 0; j < input.Length; j++)
                {
                    res += input[j].Item1 + " " + input[j].Item2 + " " + input[j].Item3;
                    if (i + 1 != input.Length)
                    {
                        res += '\n';
                        if (j + 1 == input.Length) res += '\r';
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

        private string TypeClassification(Type type)
        {
            if (type.IsEnum) return "Перечисление";
            else if (type.IsValueType) return "Структура";
            else if (type.IsInterface) return "Интерфейс";
            else if (typeof(Delegate).IsAssignableFrom(type.BaseType)) return "Делегат";
            else return "Класс";
        }

        private string CleanTypeName(Type type)
        {
            string name = type.Name.Split('+')[^1];
            int index = name.IndexOf('`');
            if (index >= 0) name = name.Substring(0, index);
            return name;
        }

        private enum Flags
        {
            StaticClass =        0b00000000001, // NotImplementation
            Delegate =           0b00000000010,
            AnonymousType =      0b00000000100, // NotImplementation
            Struct =             0b00000001000,
            Nested =             0b00000010000,
            Enum =               0b00000100000,
            Interface =          0b00001000000,
            PrivateMethods =     0b00010000000,
            StaticMethods =      0b00100000000,
            Property =           0b01000000000, // NotImplementation
            RegisterAccsessors = 0b10000000000 // NotImplementation
        }

        private struct wmc
        {

            (string, string, int)[] array;

            public wmc(int len)
            {
                array = new (string, string, int)[len];
            }

            public int Length { get => array.Length; }

            public (string, string, int) this [int index]
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
                for(int i = 0; i < array.Length; i++)
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
