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
        public void Meth();
    }

    public interface Class19
    {
        public void Meth() { }
    }

    public struct Class20
    {
        public void Meth() { }
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
        public static void Meth() { }
    }

    public static class Class27
    {
        public static void Meth() { }
    }

    public abstract class Class28
    {
        public abstract void Meth();

        public abstract void Meth(int i);

        public void Meth(string s) { }
    }

    public class Class29
    { 
        int this [int i]
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

    public class Class30
    {
        event Class24 class30Event
        {
            add
            {

            }
            remove
            {

            }
        }
       
    }

    public class Class31
    {
        event Class24 class31Event;
    }
}
