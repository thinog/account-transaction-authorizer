using System.Linq;
using TransactionAuthorizer.Application.Models;
using TransactionAuthorizer.Application.UseCases.CreateAccount;

namespace TransactionAuthorizer.UnitTests.Application.UseCases.CreateAccount
{
    public class CreateAccountOutputFaker : ICreateAccountOutput
    {
        public CreateAccountOutputFaker()
        {
            Account = new AccountModel();
        }

        public bool AccountAlreadyInitializedCalled, FillCalled;

        public AccountModel Account { get; set; }

        public bool HasErrors => Account.Violations.Any();

        public void AccountAlreadyInitialized()
        {
            AccountAlreadyInitializedCalled = true;
        }

        public void Fill(AccountDetailsModel accountDetails)
        {
            FillCalled = true;
        }
    }
}