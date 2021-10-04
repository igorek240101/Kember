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

        [SetUp]
        public void Setup()
        {
            assembly = Assembly.GetAssembly(typeof(Class1));
        }

        [Test]
        public void CountOfTypes()
        {
            (int,int)[] input = { (0b00000000, 19), (0b00000010, 20), (0b00001000, 20), (0b00010000, 20),
                                  (0b00010010, 23), (0b00100000, 20), (0b01000000, 21), (0b01111010, 27)};
            foreach(var value in input)
            {
                int res = (wmc.RunMetric(assembly, value.Item1) as (string, string, int)[]).Length;
                Assert.AreEqual(value.Item2, res, value.Item1.ToString());
            }
        }

        [Test]
        public void PrivateMethodInClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class1").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class1").Item3;
            Assert.AreEqual(0, res);
        }
    }
}