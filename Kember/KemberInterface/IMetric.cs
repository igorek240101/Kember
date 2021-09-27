using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Kember
{
    public interface IMetric
    {
        public object RunMetric(Assembly assembly, object args);

        public object Read(string input);

        public string Write(object output);
    }
}
