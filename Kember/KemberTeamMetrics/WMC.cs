using System;
using System.Linq;
using System.Reflection;
using Kember;

namespace KemberTeamMetrics
{
    public class WMC : IMetric
    {

        public object RunMetric(Assembly assembly, object args)
        {
            return assembly.GetTypes().ToList().ConvertAll(t => (t.FullName, t.GetMethods().Length)).ToArray();
        }

        public object[] Read(string input, out Assembly[] assemblies)
        {
            throw new NotImplementedException();
        }

        public string Write(object[] output, Assembly[] assemblies)
        {
            throw new NotImplementedException();
        }
    }
}
