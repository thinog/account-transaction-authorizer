using System;
using Newtonsoft.Json;
using TransactionAuthorizer.Application.Factories;

namespace TransactionAuthorizer.Presentation.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            if(Console.IsInputRedirected)
            {                
                int i = 1;
                string input;

                while((input = Console.ReadLine()) != null)
                {
                    Console.WriteLine($"Input {i++}: {input}");
                    var useCase = UseCaseFactory.CreateUseCase(input);                    
                    useCase.UseCase.Execute(useCase.InputPort);
                    var output = useCase.UseCase.ToString();
                    Console.WriteLine($"Output {i++}: {output}");
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
