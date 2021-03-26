using System;
using Newtonsoft.Json;
using TransactionAuthorizer.Application.Factories;
using TransactionAuthorizer.Infrastructure.IoC;

namespace TransactionAuthorizer.Presentation.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            if(Console.IsInputRedirected)
            {                
                // int i = 1;
                string input;

                try
                {
                    var serviceProvider = DependencyResolver.Resolve();

                    while((input = Console.ReadLine()) != null)
                    {
                        // Console.WriteLine($"Input {i++}: {input}");
                        var useCase = UseCaseFactory.CreateUseCase(input, serviceProvider);                    
                        useCase.UseCase.Execute(useCase.InputPort);
                        var output = useCase.UseCase.ToString();
                        Console.WriteLine(output);
                        // Console.WriteLine($"Output {i++}: {output}");
                    }
                }
                catch(JsonReaderException exception)
                {
                    Console.WriteLine($"Error: {exception.Message}");
                    Environment.Exit((int)ExitCodes.Error);
                }
            } 
            else 
            {
                MissingInput();
            }

            Environment.Exit((int)ExitCodes.Ok);
        }

        private static void MissingInput()
        {
            Console.WriteLine("Missing input!");
            Console.WriteLine("USAGE: $ authorize < operations_file");
        }
    }
}
