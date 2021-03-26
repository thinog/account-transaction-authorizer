using System;
using Newtonsoft.Json;
using TransactionAuthorizer.Domain.Entities;

namespace TransactionAuthorizer.Application.Models
{
    public class TransactionDetailsModel
    {
        [JsonProperty("merchant")]
        [JsonRequired]
        public string Merchant { get; set; }

        [JsonProperty("amount")]
        [JsonRequired]
        public int Amount { get; set; }

        [JsonProperty("time")]
        [JsonRequired]
        public DateTime Time { get; set; }

        public TransactionDetailsModel(Transaction transaction)
        {
            Merchant = transaction.Merchant;
            Amount = transaction.Value;
            Time = transaction.Time;
        }
    }
}