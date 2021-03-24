using System;
using Newtonsoft.Json;

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
    }
}