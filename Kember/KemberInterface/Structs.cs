using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace KemberInterface
{
    public class InvokeArgs
    {
        public Assembly[] Assembly { get; set; }
        public object Args { get; set; }
        public string Metric { get; set; }
    }

    public class Registration
    {
        public string Name { get; set; }
        public string Key { get; set; }
    }
}
