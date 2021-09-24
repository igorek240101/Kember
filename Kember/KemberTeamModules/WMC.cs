using System;
using System.Reflection;
using Kember;
using System.Linq;

namespace KemberTeamModules
{
    public class WMC : IMetric
    {
        public object RunMetric(Assembly assembly, object args)
        {
            return assembly.GetTypes().ToList().ConvertAll(t => t.GetMethods().Length).ToArray();
        }
    }
}
