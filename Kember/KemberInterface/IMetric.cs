using System.Reflection;

namespace Kember
{
    public interface IMetric
    {
        public object RunMetric(Assembly assembly, object args);

        public object[] Read(string input, out Assembly[] assemblies);

        public string Write(object[] output, Assembly[] assemblies);
    }
}
