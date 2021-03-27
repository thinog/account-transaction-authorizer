namespace TransactionAuthorizer.Application.Enums
{
    public class Violation
    {
        private Violation(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public static string AccountAlreadyInitialized => new Violation("account-already-initialized").Value;
        public static string AccountNotInitialized => new Violation("account-not-initialized").Value;
        public static string CardNotActive => new Violation("card-not-active").Value;
        public static string InsuficientLimit => new Violation("insuficient-limit").Value;
        public static string HighFrequencySmallInterval => new Violation("high-frequency-small-interval").Value;
        public static string DoubledTransaction => new Violation("doubled-transaction").Value;
    }
}