using System;

namespace WMC2Test
{
    public class Class1
    {
        private void Meth1(int i) { }
        public void Meth2(string j) { }
        protected void Meth3() { }
    }

    public class Class2 : Class1
    {
        private void Meth4(int f) { }
    }

    public class Class3
    {
        public void Meth1(int i, int f) { }

        public void Meth2() { }
    }

    public class Class4 : Class3
    {
        public void Meth3(string j) { }
    }

    public class Class5
    {
        protected void Meth1(int i, int f, string j) { }
    }

    public class Class6 : Class5
    {
        public void Meth2() { }
    }

    public class Class7 : Object
    {
        public void Meth2(object o) { }
    }

    public class Class8<T>
    {
        public void Meth2(T t) { }
    }

    public class Class9
    {
        public T Meth2<T>(T t) { return t; }
    }

    internal class Class10
    {
        public void Meth2() { }
    }

    public class Class11
    {
        public void Meth(bool b, short s, int i, string str)
        {
            var aType = new { a = 10, s = "N" };
        }
    }

    public class Class12
    {
        delegate int Class13();

        Class13 GetF = delegate ()
        {
            return 0;
        };
    }

    public class Class14
    {
        delegate int Class15(string s);

        Class15 GetF = s => s.Length;
    }

    public class Class16
    {
        public void Meth() { }
        public void Meth(string s) { }
    }

    public class Class17
    {
        public void Meth(string s = "N") { }
    }

    public interface Class18
    {
        public void Meth(int i);
    }

    public interface Class19
    {
        public void Meth(float f) { }
    }

    public struct Class20
    {
        public void Meth(decimal d) { }
    }

    public enum Class21
    {
        S, M = 1, T = 10, F, St = 9
    }

    public class Class22
    {
        public void Method() { }
        public class Class23
        {
            public void Method() { }
        }
    }

    delegate int Class24(string s);

    public class Class25
    {
        public int Property
        {
            get { return 0; }
            set { return; }
        }
    }

    public class Class26
    {
        public static void Meth(int k, bool j) { }
    }

    public static class Class27
    {
        public static void Meth(string s) { }
    }

    public abstract class Class28
    {
        public abstract void Meth();

        public abstract void Meth(int i);

        public void Meth(string s, float f) { }
    }

    public class Class29
    {
        int this[int i]
        {
            get
            {
                return i;
            }
            set
            {
                return;
            }
        }
    }
}
