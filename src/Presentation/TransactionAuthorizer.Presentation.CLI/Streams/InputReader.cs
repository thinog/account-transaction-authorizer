using System;
using System.IO;

namespace TransactionAuthorizer.Presentation.CLI.Streams
{
    public class InputReader : IDisposable
    {
        private TextReader _defaultReader;

        public InputReader()
        {            
            _defaultReader = Console.In;
        }

        public InputReader(TextReader reader)
        {
            _defaultReader = Console.In;
            Console.SetIn(reader);
        }

        public bool HasInput()
        {
            return Console.IsInputRedirected;
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Dispose()
        {
            Console.SetIn(_defaultReader);
        }
    }
}