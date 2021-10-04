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
            int res = (wmc.RunMetric(assembly, 0b00000000) as (string, int)[]).Length;
            Assert.AreEqual(21, res, "0b00000000");
            res = (wmc.RunMetric(assembly, 0b00000010) as (string, int)[]).Length;
            Assert.AreEqual(22, res, "0b00000010");
            res = (wmc.RunMetric(assembly, 0b00001000) as (string, int)[]).Length;
            Assert.AreEqual(22, res, "0b00001000");
            res = (wmc.RunMetric(assembly, 0b00010000) as (string, int)[]).Length;
            Assert.AreEqual(22, res, "0b00010000");
            res = (wmc.RunMetric(assembly, 0b00010010) as (string, int)[]).Length;
            Assert.AreEqual(25, res, "0b00010010");
            //res = (wmc.RunMetric(assembly, 0b00100000) as (string, int)[]).Length;
            //Assert.AreEqual(22, res, "0b00100000");
            res = (wmc.RunMetric(assembly, 0b00111010) as (string, int)[]).Length;
            Assert.AreEqual(27, res, "0b00111010");

            /*
            (string, int)[] res1 = (wmc.RunMetric(assembly, 0b00100000) as (string, int)[]);
            string s = "";
            foreach (var value in res1)
            {
                s += "\n" + value.Item1;
            }
            throw new System.Exception(s);
            */
        }
    }
}