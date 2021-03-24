using TransactionAuthorizer.Domain.Interfaces.UseCases;
using TransactionAuthorizer.Application.Models;
using Newtonsoft.Json;

namespace TransactionAuthorizer.Application.UseCases.AuthorizeTransaction
{
    public class AuthorizeTransactionInput : IInputPort
    {
        [JsonProperty("transaction")]
        [JsonRequired]
        public TransactionDetailsModel Transaction { get; set; }
    }
}