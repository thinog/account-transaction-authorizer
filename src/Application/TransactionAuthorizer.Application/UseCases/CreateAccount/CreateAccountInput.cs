using TransactionAuthorizer.Domain.Interfaces.UseCases;
using TransactionAuthorizer.Application.Models;
using Newtonsoft.Json;

namespace TransactionAuthorizer.Application.UseCases.CreateAccount
{
    public class CreateAccountInput : IInputPort
    {
        [JsonProperty("account")]
        [JsonRequired]
        public AccountDetailsModel Account { get; set; }
    }
}