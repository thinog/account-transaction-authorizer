using TransactionAuthorizer.Application.Enums;
using TransactionAuthorizer.Application.Models;

namespace TransactionAuthorizer.Application.UseCases.AuthorizeTransaction
{
    public class AuthorizeTransactionDefaultOutput : IAuthorizeTransactionOutput
    {
        public AuthorizeTransactionDefaultOutput()
        {
            Account = new AccountModel();
        }

        public AccountModel Account { get; set; }

        public void Ok(AccountDetailsModel accountDetails)
        {
            Account.AccountDetails = accountDetails;
        }

        public void AccountNotInitialized()
        {
            Account.Violations.Add(Violation.AccountNotInitialized.Value);
        }

        public void CardNotActive()
        {
            Account.Violations.Add(Violation.CardNotActive.Value);
        }

        public void DoubledTransaction()
        {
            Account.Violations.Add(Violation.DoubledTransaction.Value);
        }

        public void HighFrequencySmallInterval()
        {
            Account.Violations.Add(Violation.HighFrequencySmallInterval.Value);
        }

        public void InsufficientLimit()
        {
            Account.Violations.Add(Violation.InsuficientLimit.Value);
        }
    }
}