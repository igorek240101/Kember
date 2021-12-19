using NUnit.Framework;
using System.Reflection;
using System.Linq;
using TestApp1;
using KemberTeamMetrics;

namespace KemberMetricTest
{
    public class WMC
    {
        /*

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
            (int,int)[] input = { (0b00000000, 23), (0b00000010, 24), (0b00001000, 24), (0b00010000, 24),
                                  (0b00010010, 27), (0b00100000, 24), (0b01000000, 25), (0b01111010, 31)};
            foreach(var value in input)
            {
                int res = (wmc.RunMetric(assembly, value.Item1) as (string, string, int)[]).Length;
                Assert.AreEqual(value.Item2, res, value.Item1.ToString());
            }
        }

        [Test]
        public void TypeName()
        {
            (string, string)[] input = {("�����","Class1"),         ("�����","Class2"),      ("�����","Class3"),      ("�����","Class4"),
                                        ("�����","Class5"),         ("�����","Class6"),      ("�����","Class7"),      ("�����","Class8"),
                                        ("�����","Class9"),         ("�����","Class10"),     ("�����","Class11"),     ("�����","Class12"),
                                        ("�������","Class13"),      ("�����","Class14"),     ("�������","Class15"),   ("�����","Class16"),
                                        ("�����","Class17"),        ("���������","Class18"), ("���������","Class19"), ("���������","Class20"),
                                        ("������������","Class21"), ("�����","Class22"),     ("�����","Class23"),     ("�������","Class24"),
                                        ("�����","Class25"),        ("�����","Class26"),     ("�����","Class27"),     ("�����","Class28")};
            (string, string, int)[] res = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            foreach (var value in input)
            {
                Assert.NotNull(res.FirstOrDefault(t => t.Item1 == value.Item1 && t.Item2 == value.Item2));
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

        [Test]
        public void PrivateMethodInClassWithPrivateSuperClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class2").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class2").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void PublicMethodInClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class3").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class3").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void PublicMethodInClassWithPublicSuperClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class4").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class4").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void ProtectedMethodInClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class5").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class5").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void PublicMethodInClassWithProtectedSuperClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class6").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class6").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void PublicMethodInClassWithObject()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class7").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class7").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void MethodInGenericClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class8").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void GenericMethod()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class9").Item3;
            Assert.AreEqual(1, res);
        }


        [Test]
        public void PublicMethodInInternalClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class10").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class10").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void MethodWithAType()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class11").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void ClassWithNestedDelegate()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class12").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void Delegate()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class13").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void ClassWithNestedLDelegate()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class14").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void LDelegate()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class15").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void Overloading()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class16").Item3;
            Assert.AreEqual(2, res);
        }

        [Test]
        public void OverloadingWithDefault()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class17").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void Interface()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class18").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void InterfaceWithDefault()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class19").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void Struct()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class20").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void Enum()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class21").Item3;
            Assert.AreEqual(0, res);
        }


        [Test]
        public void ClassWithNestedClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class22").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void NestedClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class23").Item3;
            Assert.AreEqual(1, res);
        }

        [Test]
        public void ExtendDelegate()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class24").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void Property()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b00111111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class25").Item3;
            Assert.AreEqual(0, res);
            metrics = (wmc.RunMetric(assembly, 0b01111111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class25").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b10111111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class25").Item3;
            Assert.AreEqual(0, res);
            metrics = (wmc.RunMetric(assembly, 0b11111111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class25").Item3;
            Assert.AreEqual(2, res);
        }


        [Test]
        public void StaticMeth()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b111111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class26").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class26").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void StaticClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b111111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class27").Item3;
            Assert.AreEqual(1, res);
            metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class27").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void AbstractClass()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class28").Item3;
            Assert.AreEqual(3, res);
        }

        [Test]
        public void Indexer()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class29").Item3;
            Assert.AreEqual(2, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class29").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void Eventer()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class30").Item3;
            Assert.AreEqual(2, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class30").Item3;
            Assert.AreEqual(0, res);
        }

        [Test]
        public void EventerWithoutAccessor()
        {
            (string, string, int)[] metrics = (wmc.RunMetric(assembly, 0b11111010) as (string, string, int)[]);
            int res = metrics.FirstOrDefault(t => t.Item2 == "Class31").Item3;
            Assert.AreEqual(0, res);
            metrics = (wmc.RunMetric(assembly, 0b01111010) as (string, string, int)[]);
            res = metrics.FirstOrDefault(t => t.Item2 == "Class31").Item3;
            Assert.AreEqual(0, res);
        }

        */
    }
}