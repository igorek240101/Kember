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
            Flags flags = (Flags)(args as int?).Value;
            List<Type> types = CleanType(assembly, flags);
            (string, string, int)[] res = new (string, string, int)[types.Count];
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
                    int plas = (flags & Flags.Property) != 0 ? types[i].GetProperties(binding).Length : 0;
                    res[i] = (TypeClassification(types[i]), CleanTypeName(types[i]), types[i].GetMethods(binding).Length + plas);
                }
            }
            return res;
        }

        public (object, Assembly)[] Read(string input)
        {
            throw new NotImplementedException();
        }

        public string Write((object, Assembly)[] output)
        {
            throw new NotImplementedException();
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
            else if (type.IsAbstract) return "Абстрактный класс";
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
            StaticClass =    0b0000000001, // NotImplementation
            Delegate =       0b0000000010,
            AnonymousType =  0b0000000100, // NotImplementation
            Struct =         0b0000001000,
            Nested =         0b0000010000,
            Enum =           0b0000100000,
            Interface =      0b0001000000,
            PrivateMethods = 0b0010000000,
            StaticMethods =  0b0100000000,
            Property =       0b1000000000
        }
    }
}
