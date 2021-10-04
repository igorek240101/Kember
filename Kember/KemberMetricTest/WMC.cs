using NUnit.Framework;
using System.Reflection;
using TestApp1;

namespace KemberMetricTest
{
    public class WMC
    {

        Assembly assembly;

        [SetUp]
        public void Setup()
        {
            assembly = Assembly.GetAssembly(typeof(Class1));

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}