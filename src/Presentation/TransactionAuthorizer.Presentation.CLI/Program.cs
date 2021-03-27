using System;
using Newtonsoft.Json;
using TransactionAuthorizer.Application.Factories;
using TransactionAuthorizer.Infrastructure.IoC;
using TransactionAuthorizer.Presentation.CLI.Enums;
using TransactionAuthorizer.Presentation.CLI.Streams;

namespace TransactionAuthorizer.Presentation.CLI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var reader = new InputReader();
            var writer = new OutputWriter();

            if (reader.HasInput())
            {
                var serviceProvider = DependencyResolver.Resolve();
                Process(serviceProvider, reader, writer);
            }
            else
            {
                writer.WriteLine("Missing input!");
                writer.WriteLine("USAGE: $ authorize < operations_file");
            }

            Environment.Exit((int)ExitCodes.Ok);
        }

        public static void Process(IServiceProvider serviceProvider, InputReader reader, OutputWriter writer)
        {
            string input;

            while ((input = reader.ReadLine()) != null)
            {
                try
                {
                    var useCase = UseCaseFactory.CreateUseCase(input, serviceProvider);
                    var inputPort = UseCaseFactory.CreateInputPort(input, useCase);
                    var output = useCase.Execute(inputPort);

                    writer.WriteLine(output);
                }
                catch (JsonReaderException exception)
                {
                    writer.WriteLine($"Error: {exception.Message}");
                    Environment.Exit((int)ExitCodes.Error);
                }
                // catch (Exception ex)
                catch (Exception)
                {
                    writer.WriteLine($"An error has occurred!");
                    // writer.WriteLine(ex.Message);
                    // writer.WriteLine(ex.StackTrace);
                    Environment.Exit((int)ExitCodes.Error);
                }
            }
        }
    }
}
