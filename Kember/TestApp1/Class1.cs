using System;

namespace TestApp1
{
    public class Class1
    {
        private void Meth1()
        {

        }
    }

    public class Class2 : Class1
    {
        private void Meth2()
        {

        }
    }

    public class Class3
    {
        public void Meth1()
        {

        }
    }

    public class Class4 : Class3
    {
        public void Meth2()
        {
        }
    }

    public class Class5
    {
        protected void Meth1()
        {

        }
    }

    public class Class6 : Class5
    {
        public void Meth2()
        {

        }
    }

    public class Class7 : Object
    {
        public void Meth2()
        {

        }
    }

    public class Class8<T>
    {
        public void Meth2()
        {

        }
    }

    public class Class9
    {
        public void Meth2<T>()
        {

        }
    }

    internal class Class10
    {
        public void Meth2()
        {
        }
    }

    public class Class11
    {
        public void Meth()
        {
            var f = new { a = 10, s = "N" };
        }
    }

    public class Class12
    {
        delegate int F();

        F GetF = delegate ()
        {
            return 0;
        };
    }

    public class Class13
    {
        delegate int F(string s);

        F GetF = s => s.Length;
    }

    public class Class14
    {
        public void Meth() { }
        public void Meth(string s) { }
    }

    public class Class15
    {
        public void Meth(string s = "N") { }
    }

    public interface Class16
    {
        public void Meth();
    }

    public interface Class17
    {
        public void Meth() { }
    }

    public struct Class18
    {
        public void Meth() { }
    }

    public enum Class19
    {
        S, M = 1, T = 10, F, St = 9
    }
}
