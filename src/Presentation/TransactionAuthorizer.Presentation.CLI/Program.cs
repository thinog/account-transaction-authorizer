using System;

namespace TransactionAuthorizer.Presentation.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            if(Console.IsInputRedirected)
            {                
                int i = 1;
                string line;

                while((line = Console.ReadLine()) != null)
                {
                    Console.Write($"Line {i++}: ");
                    Console.WriteLine(line);                
                }
            } 
            else 
            {
                MissingInput();
            }
        }

        private static void MissingInput()
        {
            Console.WriteLine("Missing input!");
            Console.WriteLine("USAGE: $ authorize < operations_file");
        }
    }
}
