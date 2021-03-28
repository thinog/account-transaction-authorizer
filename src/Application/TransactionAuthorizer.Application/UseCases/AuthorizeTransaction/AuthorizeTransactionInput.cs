using TransactionAuthorizer.Domain.Interfaces.UseCases;
using TransactionAuthorizer.Application.Models;
using Newtonsoft.Json;
using TransactionAuthorizer.Domain.Entities;
using System;

namespace TransactionAuthorizer.Application.UseCases.AuthorizeTransaction
{
    public class AuthorizeTransactionInput : IInputPort
    {
        [JsonProperty("transaction")]
        [JsonRequired]
        public TransactionDetailsModel Transaction { get; set; }

        public AuthorizeTransactionInput() { }
        
        public AuthorizeTransactionInput(Transaction transaction)
        {
            Transaction = new TransactionDetailsModel(transaction);
        }

        public Transaction ToTransactionEntity()
        {
            return new Transaction
            {
                Merchant = Transaction?.Merchant,
                Value = Transaction?.Amount ?? 0,
                Time = Transaction?.Time ?? DateTime.MinValue
            };
        }
    }
}