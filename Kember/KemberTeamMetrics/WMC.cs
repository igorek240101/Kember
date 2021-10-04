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
            List<Type> types = CleanType(assembly, (Flags)(args as byte?).Value);
            return types.ConvertAll(t => (t.FullName, t.GetMethods().Length)).ToArray();
        }

        public object[] Read(string input, out Assembly[] assemblies)
        {
            throw new NotImplementedException();
        }

        public string Write(object[] output, Assembly[] assemblies)
        {
            throw new NotImplementedException();
        }

        private List<Type> CleanType(Assembly assembly, Flags flags)
        {
            List<Type> types = assembly.GetTypes().ToList();
            for (int i = 0; i < types.Count; i++)
            {
                if ((byte)(flags & Flags.StaticClass) == 0 && types[i].IsAbstract && types[i].IsSealed && false) // Требуется уточнение подхода для языков отличных от C$
                {
                    types.RemoveAt(i);
                    i--; continue;
                }
                else
                {
                    if ((byte)(flags & Flags.Delegate) == 0 && typeof(Delegate).IsAssignableFrom(types[i].BaseType)) // Костыль
                    {
                        types.RemoveAt(i);
                        i--; continue;
                    }
                    else
                    {
                        if ((byte)(flags & Flags.AnonymousType) == 0 && false) // В явном виде не может быть реализовано
                        {
                            types.RemoveAt(i);
                            i--; continue;
                        }
                        else
                        {
                            if ((byte)(flags & Flags.Struct) == 0 && types[i].IsValueType)
                            {
                                types.RemoveAt(i);
                                i--; continue;
                            }
                            else
                            {
                                if ((byte)(flags & Flags.Nested) == 0 && types[i].IsNested)
                                {
                                    types.RemoveAt(i);
                                    i--; continue;
                                }
                                else
                                {
                                    if ((byte)(flags & Flags.Enum) == 0 && types[i].IsEnum)
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
            return types;
        }

        private enum Flags
        {
            StaticClass =       0b00000001, // NotImplementation
            Delegate =          0b00000010,
            AnonymousType =     0b00000100, // NotImplementation
            Struct =            0b00001000,
            Nested =            0b00010000,
            Enum =              0b00100000,
            PrivateMethods =    0b01000000,
            StaticMethods =     0b10000000,
        }
    }
}
