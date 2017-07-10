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
        [JsonProperty(PropertyName = "val")]
        public string Value { get; set; }
    }
}
