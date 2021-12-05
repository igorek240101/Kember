using Kember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace KemberTeamMetrics
{
    class DIT : Metric
    {
        public override metric[] RunMetric(Assembly[] assemblies, object args)
        {
            metric[] mainRes = new metric[assemblies.Length];
            for (int k = 0; k < mainRes.Length; k++)
            {
                Assembly assembly = assemblies[k];
                List<Type> types = CleanType(assembly);
                dit res = new dit(types.Count);
                for (int i = 0; i < types.Count; i++)
                {
                    int count = 1;
                    Type type = types[i];
                    while (type.BaseType != null)
                    {
                        count++;
                        type = type.BaseType;
                    }
                    res[i] = (types[i].Name, count);
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
                dit dits = new dit(assembly.Length - 1);
                for (int j = 1; j < assembly.Length; j++)
                {
                    string[] dit = assembly[j].Split(' ');
                    dits[j - 1] = (dit[0], int.Parse(dit[1]));
                }
                res[i].obj = dits;
            }
            return res;
        }

        public override string Write(metric[] output)
        {
            string res = "";
            for (int i = 0; i < output.Length; i++)
            {
                res += output[i].assembly + '\n';
                dit input = (dit)output[i].obj;
                for (int j = 0; j < input.Length; j++)
                {
                    res += input[j].Item1 + " " + input[j].Item2;
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

        private List<Type> CleanType(Assembly assembly)
        {
            List<Type> types = assembly.GetTypes().ToList();
            for (int i = 0; i < types.Count; i++)
            {
                if (!types[i].GetTypeInfo().IsClass)
                {
                    types.RemoveAt(i);
                    i--; continue;
                }
                object[] array = types[i].GetCustomAttributes(typeof(CompilerGeneratedAttribute), false);
                if (array != null && array.Length > 0)
                {
                    types.RemoveAt(i);
                    i--; continue;
                }
            }
            return types;
        }

        private struct dit
        {
            (string, int)[] array;

            public dit(int len)
            {
                array = new (string, int)[len];
            }

            public int Length { get => array.Length; }

            public (string, int) this[int index]
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
                        s += $"{array[i].Item1} - {array[i].Item2}";
                    }
                    else
                    {
                        s += $"{array[i].Item1} - {array[i].Item2}" + ((char)0);
                    }
                }
                return s;
            }
        }
    }
}
