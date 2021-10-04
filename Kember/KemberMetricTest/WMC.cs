using NUnit.Framework;
using System.Reflection;
using TestApp1;
using KemberTeamMetrics;

namespace KemberMetricTest
{
    public class WMC
    {

        Assembly assembly;
        KemberTeamMetrics.WMC wmc = new KemberTeamMetrics.WMC();

        [SetUp]
        public void Setup()
        {
            assembly = Assembly.GetAssembly(typeof(Class1));

        }

        [Test]
        public void Test1()
        {
            (string, MethodInfo[])[] res = (wmc.RunMetric(assembly, null) as (string, MethodInfo[])[]);
            string s = "";
            foreach (var value in res)
            {
                s += value.Item1 + "\n";
                foreach (var meth in value.Item2) s += "    " + meth.Name + "\n" ;
            }
            throw new System.Exception(s);
            Assert.AreEqual(19, (wmc.RunMetric(assembly, null) as (string, int)[]).Length);
        }
    }
}