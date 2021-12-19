using System;

namespace DITTest
{

    interface IWoreker { }

    // 2
    public class Animal { }

    public class Machine { }

    internal class Math { }

    // 3
    public class Bird : Animal { }

    public class Mammals : Animal { }

    public class Fish : Animal { }

    public class Car : Machine { }

    public class Plane : Machine { }

    internal class Graph : Math { }

    // 4

    public class Cat : Mammals { }

    public class Dog : Mammals { }

    public class Mouse : Mammals { }

    public class Human : Mammals, IWoreker { }

    // 5 

    internal class Student : Human { class StudyGraph : Graph {  } /*4*/ }

    // Not suported

    public struct Structer { }

    delegate void DName();

    public enum Enum { }



}
