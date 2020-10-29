using System;
using Test;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Class1 b = new Class1();
            foreach (var z in b.GetAdmins())
            {
                Console.WriteLine("{0} {1}",z.Adminid,z.Email);
            }
        }
    }
}
