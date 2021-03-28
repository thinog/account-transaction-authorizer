using System;
using Mono.Options;
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

            try
            {
                HandleArguments(args);

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
            catch (Exception exception)
            {
                writer.WriteLine($"An error has occurred!");
                writer.WriteDebug(exception.Message);
                writer.WriteDebug(exception.StackTrace);

                Environment.Exit((int)ExitCodes.Error);
            }
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
                }
            }
        }

        public static void HandleArguments(string[] args)
        {
            var options = new OptionSet
            {
                { "verbose|v", "increases verbosity", verbose => Environment.SetEnvironmentVariable("VERBOSE", Boolean.TrueString) }
            };

            options.Parse(args);
        }
    }
}
