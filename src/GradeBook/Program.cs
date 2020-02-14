using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            double x = 30.3;
            double y = 44.23;

            double z = x + y;

            if(args.Length > 0)
            {
                Console.WriteLine($"Hello, {args[0]}!");
                Console.WriteLine(z);
            }
            else
            {
                Console.WriteLine("Hello!");
            }
        }
    }
}
