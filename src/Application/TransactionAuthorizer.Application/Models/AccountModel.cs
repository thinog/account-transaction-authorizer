using System.Collections.Generic;
using Newtonsoft.Json;

namespace TransactionAuthorizer.Application.Models
{
    public class AccountModel
    {
        [JsonProperty("account")]
        public AccountDetailsModel AccountDetails { get; set; }

        [JsonProperty("violations")]
        public List<string> Violations { get; set; }

        public AccountModel()
        {
            Violations = new List<string>();
        }
    }
}