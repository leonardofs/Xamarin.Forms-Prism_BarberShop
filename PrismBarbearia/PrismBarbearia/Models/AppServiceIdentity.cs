using Newtonsoft.Json;
using System.Collections.Generic;

namespace PrismBarbearia.Models
{
    public class AppServiceIdentity
    {
        [JsonProperty(PropertyName = "user_claims")]
        public List<UserClaim> UserClaims { get; set; }
    }

    public class UserClaim
    {
        [JsonProperty(PropertyName = "typ")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "val")]
        public string Value { get; set; }
    }
}
