using System.Linq;
using Newtonsoft.Json;
using TransactionAuthorizer.Application.Enums;
using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Domain.Utils;

namespace TransactionAuthorizer.Application.UseCases.CreateAccount
{
    public class CreateAccountDefaultOutput : ICreateAccountOutput
    {
        public CreateAccountDefaultOutput()
        {
            Account = new AccountModel();
        }

        public AccountModel Account { get; set; }

        public bool HasErrors => Account.Violations.Any();

        public void AccountAlreadyInitialized()
        {
            Account.Violations.AddIfNotExists(Violation.AccountAlreadyInitialized);
        }

        public void Fill(AccountDetailsModel accountDetails)
        {
            Account.AccountDetails = accountDetails;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(Account);
        }
    }
}