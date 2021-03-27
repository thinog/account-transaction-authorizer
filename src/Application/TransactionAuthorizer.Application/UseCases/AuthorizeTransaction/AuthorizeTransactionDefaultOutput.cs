using System.Linq;
using Newtonsoft.Json;
using TransactionAuthorizer.Application.Enums;
using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Domain.Utils;

namespace TransactionAuthorizer.Application.UseCases.AuthorizeTransaction
{
    public class AuthorizeTransactionDefaultOutput : IAuthorizeTransactionOutput
    {
        public AuthorizeTransactionDefaultOutput()
        {
            Account = new AccountModel();
        }

        public AccountModel Account { get; set; }
        public bool HasErrors => Account.Violations.Any();

        public void Ok(AccountDetailsModel accountDetails)
        {
            Account.AccountDetails = accountDetails;
        }

        public void AccountNotInitialized()
        {
            Account.Violations.AddIfNotExists(Violation.AccountNotInitialized);
        }

        public void CardNotActive()
        {
            Account.Violations.AddIfNotExists(Violation.CardNotActive);
        }

        public void DoubledTransaction()
        {
            Account.Violations.AddIfNotExists(Violation.DoubledTransaction);
        }

        public void HighFrequencySmallInterval()
        {
            Account.Violations.AddIfNotExists(Violation.HighFrequencySmallInterval);
        }

        public void InsufficientLimit()
        {
            Account.Violations.AddIfNotExists(Violation.InsuficientLimit);
        }        

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Account);
        }
    }
}