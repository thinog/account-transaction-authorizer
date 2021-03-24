namespace TransactionAuthorizer.Application.Enums
{
    public class Violation
    {
        private Violation(string value)
        {
            Value = value;
        }

        public string Value { get; set; }

        public static Violation AccountAlreadyInitialized => new Violation("account-already-initialized");
        public static Violation AccountNotInitialized => new Violation("account-not-initialized");
        public static Violation CardNotActive => new Violation("card-not-active");
        public static Violation InsuficientLimit => new Violation("insuficient-limit");
        public static Violation HighFrequencySmallInterval => new Violation("high-frequency-small-interval");
        public static Violation DoubledTransaction => new Violation("doubled-transaction");
    }
}