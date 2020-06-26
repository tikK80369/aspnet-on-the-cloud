#pragma warning disable 0436
using System;
using MathLibrary;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            int row = 0;

            do
            {
                if (row == 0 || row >= 25)
                    ResetConsole();
                Console.WriteLine("Please enter two numbers.");

                try{
                    string input = Console.ReadLine();
                    if (String.IsNullOrEmpty(input)) break;
                    row += 1;
                    double a = double.Parse(input);
                    input = Console.ReadLine();
                    if (String.IsNullOrEmpty(input)) break;
                    row += 1;
                    double b = double.Parse(input);
                    Console.WriteLine($"{a} + {b} = {MyMath.Add(a, b)}");
                    Console.WriteLine($"{a} - {b} = {MyMath.Subtract(a, b)}");
                    Console.WriteLine($"{a} * {b} = {MyMath.Multiply(a, b)}");
                    Console.WriteLine($"{a} / {b} = {MyMath.Divide(a, b)}");
                    row += 4;
                }
                catch
                {
                    Console.WriteLine("Please don't enter strings.");
                }
            } while (true);
            return;

            // Declare a ResetConsole local method
            void ResetConsole()
            {
                if (row > 0) {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                }
                Console.Clear();
                Console.WriteLine("\nPress <Enter> only to exit; otherwise, enter a string and press <Enter>:\n");
                row = 3;
            }
        }
    }
}
