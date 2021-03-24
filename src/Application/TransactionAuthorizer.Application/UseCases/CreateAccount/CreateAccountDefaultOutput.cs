using TransactionAuthorizer.Application.Enums;
using TransactionAuthorizer.Application.Models;

namespace TransactionAuthorizer.Application.UseCases.CreateAccount
{
    public class CreateAccountDefaultOutput : ICreateAccountOutput
    {
        public CreateAccountDefaultOutput()
        {
            Account = new AccountModel();
        }

        public AccountModel Account { get; set; }

        public void AccountAlreadyInitialized()
        {
            Account.Violations.Add(Violation.AccountAlreadyInitialized.Value);
        }

        public void Ok(AccountDetailsModel accountDetails)
        {
            Account.AccountDetails = accountDetails;
        }
    }
}