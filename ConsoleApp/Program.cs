using Models;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person { FirstName = "Ewa", LastName = "Ewowska" };

            Console.WriteLine($"Hello {person.FirstName}!");
        }
    }
}
