using System.Reflection;

namespace Kember
{
    public interface IMetric
    {
        public object RunMetric(Assembly assembly, object args);

        public (object, string)[] Read(string input);

        public string Write((object, string)[] output);
    }
}
