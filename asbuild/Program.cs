using System;

namespace MyConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided.");
                return;
            }

            Console.WriteLine("Arguments provided:");
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            // Example: Handling specific arguments
            if (args.Length >= 2 && args[0] == "--name")
            {
                string name = args[1];
                Console.WriteLine($"Hello, {name}!");
            }
        }
    }
}


// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
