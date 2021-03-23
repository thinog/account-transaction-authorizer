using System;

namespace TransactionAuthorizer.Presentation.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;

            Console.WriteLine("Starting...");            

            string line;

            do
            {
                line = Console.ReadLine();
                Console.Write($"Arg {i}: ");
                Console.WriteLine(line);
                Console.WriteLine($"IsInputRedirected: {Console.IsInputRedirected}");
            } while(!string.IsNullOrWhiteSpace(line));
        }
    }
}
