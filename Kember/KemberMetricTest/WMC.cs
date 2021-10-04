using NUnit.Framework;
using System.Reflection;
using System.Linq;
using TestApp1;
using KemberTeamMetrics;

namespace KemberMetricTest
{
    public class WMC
    {

        Assembly assembly;
        KemberTeamMetrics.WMC wmc = new KemberTeamMetrics.WMC();
        (string, int)[] metrics;

        [SetUp]
        public void Setup()
        {
            assembly = Assembly.GetAssembly(typeof(Class1));
            metrics = (wmc.RunMetric(assembly, 0b1111010) as (string, int)[]);
        }

        [Test]
        public void CountOfTypes()
        {
            (int,int)[] input = { (0b00000000, 19), (0b00000010, 20), (0b00001000, 20), (0b00010000, 20),
                                  (0b00010010, 23), (0b00100000, 20), (0b01000000, 21), (0b01111010, 27)};
            foreach(var value in input)
            {
                int res = (wmc.RunMetric(assembly, value.Item1) as (string, int)[]).Length;
                Assert.AreEqual(value.Item2, res, value.Item1.ToString());
            }
        }

        [Test]
        public void PrivateMethodInClass()
        {
            int res = metrics.FirstOrDefault(t => t.Item1 == "Class1").Item2;
            Assert.AreEqual(1, res);
        }
    }
}