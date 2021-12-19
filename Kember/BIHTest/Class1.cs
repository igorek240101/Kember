using System;

namespace BIHTest
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
        public void Meth1() { }

        private void Meth2() { }
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

        public void Meth3() { }

        public void Meth4() { }

        private void Meth5() { }
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

    public class Class16
    {
        public void Meth() { }
        private void Meth(string s, Type t) { }
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
        protected void Meth(float f) { }
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
            private get { return 0; }
            set { return; }
        }
    }
    public static class Class27
    {
        public static void Meth(string s) { }
    }

    public abstract class Class28
    {
        public abstract void Meth(int i, string s, float f, float ff, decimal d);

        protected abstract void Meth(int i, string s, float f, float ff);

        private void Meth(string s, float f) { }
    }

    public class Class29
    {
        private int this[int i]
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
