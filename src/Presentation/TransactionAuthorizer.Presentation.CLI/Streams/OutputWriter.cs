using System;
using System.IO;

namespace TransactionAuthorizer.Presentation.CLI.Streams
{
    public class OutputWriter : IDisposable
    {
        private TextWriter _defaultWriter;

        public OutputWriter()
        {
            _defaultWriter = Console.Out;
        }

        public OutputWriter(TextWriter writer)
        {
            _defaultWriter = Console.Out;
            Console.SetOut(writer);
        }

        public void WriteLine(object value)
        {
            Console.WriteLine(value);
        }

        public void WriteDebug(object value)
        {
            if (Environment.GetEnvironmentVariable("VERBOSE") == Boolean.TrueString)
                Console.WriteLine(value);
        }

        public void Dispose()
        {
            Console.SetOut(_defaultWriter);
        }
    }
}