using Newtonsoft.Json;

namespace TransactionAuthorizer.Application.Models
{
    public class AccountDetailsModel
    {
        [JsonProperty("active-card")]
        [JsonRequired]
        public bool ActiveCard { get; set; }

        [JsonProperty("available-limit")]
        [JsonRequired]
        public int AvailableLimit { get; set; }
    }
}