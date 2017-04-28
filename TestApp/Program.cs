using System;

namespace TestApp
{
    class Program
    {
        enum Stuff
        {
            something,
            other,
            than,
            that
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"{Stuff.something.ToString()}");
        }
    }
}