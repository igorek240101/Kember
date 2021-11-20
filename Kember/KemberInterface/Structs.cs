using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace KemberInterface
{
    public struct InvokeArgs
    {
        public Assembly[] assembly;
        public object args;
        public string metric;
    }
}
