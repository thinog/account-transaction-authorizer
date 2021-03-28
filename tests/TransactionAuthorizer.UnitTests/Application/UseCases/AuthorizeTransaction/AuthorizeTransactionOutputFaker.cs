using System.Linq;
using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Application.UseCases.AuthorizeTransaction;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.AuthorizeTransaction
{
    public class AuthorizeTransactionOutputFaker : IAuthorizeTransactionOutput
    {
        public AuthorizeTransactionOutputFaker()
        {
            Account = new AccountModel();
        }

        public AccountModel Account { get; set; }

        public bool HasErrors => Account.Violations.Any();

        public bool 
            AccountNotInitializedCalled, 
            CardNotActiveCalled, 
            DoubledTransactionCalled,
            FillCalled,
            HighFrequencySmallIntervalCalled,
            InsufficientLimitCalled;

        public void AccountNotInitialized()
        {
            AccountNotInitializedCalled = true;
        }

        public void CardNotActive()
        {
            CardNotActiveCalled = true;
        }

        public void DoubledTransaction()
        {
            DoubledTransactionCalled = true;
        }

        public void Fill(AccountDetailsModel accountDetails)
        {
            FillCalled = true;
        }

        public void HighFrequencySmallInterval()
        {
            HighFrequencySmallIntervalCalled = true;
        }

        public void InsufficientLimit()
        {
            InsufficientLimitCalled = true;
        }
    }
}