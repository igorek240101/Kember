using Kember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace KemberTeamMetrics
{
    class NOC : Metric
    {
        public override metric[] RunMetric(Assembly[] assemblies, object args)
        {
            List<Type> allTypies = new List<Type>();
            foreach(var value in assemblies)
            {
                allTypies.AddRange(value.GetTypes());
            }
            metric[] mainRes = new metric[assemblies.Length];
            for (int k = 0; k < mainRes.Length; k++)
            {
                Assembly assembly = assemblies[k];
                List<Type> types = CleanType(assembly);
                noc res = new noc(types.Count);
                for (int i = 0; i < types.Count; i++)
                {
                    int count = 0;
                    foreach(var value in allTypies)
                    {
                        if (types[i] == value.BaseType) count++;
                    }
                    res[i] = (CleanTypeName(types[i]), count);
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
                noc nocs = new noc(assembly.Length - 1);
                for (int j = 1; j < assembly.Length; j++)
                {
                    string[] noc = assembly[j].Split(' ');
                    nocs[j - 1] = (noc[0], int.Parse(noc[1]));
                }
                res[i].obj = nocs;
            }
            return res;
        }

        public override string Write(metric[] output)
        {
            string res = "";
            for (int i = 0; i < output.Length; i++)
            {
                res += output[i].assembly + '\n';
                noc input = (noc)output[i].obj;
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
                if (!types[i].GetTypeInfo().IsClass || typeof(Delegate).IsAssignableFrom(types[i].BaseType))
                {
                    types.RemoveAt(i);
                    i--;
                    continue;
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

        private struct noc
        {
            (string, int)[] array;

            public noc(int len)
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
