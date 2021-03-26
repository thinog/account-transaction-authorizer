using TransactionAuthorizer.Domain.Interfaces.UseCases;
using TransactionAuthorizer.Application.Models;
using Newtonsoft.Json;
using TransactionAuthorizer.Domain.Entities;

namespace TransactionAuthorizer.Application.UseCases.CreateAccount
{
    public class CreateAccountInput : IInputPort
    {
        [JsonProperty("account")]
        [JsonRequired]
        public AccountDetailsModel Account { get; set; }

        public CreateAccountInput(Account account)
        {
            Account = new AccountDetailsModel(account);
        }

        public Account ToAccountEntity()
        {
            return new Account
            {
                Active = Account.ActiveCard,
                Limit = Account.AvailableLimit
            };
        }
    }
}