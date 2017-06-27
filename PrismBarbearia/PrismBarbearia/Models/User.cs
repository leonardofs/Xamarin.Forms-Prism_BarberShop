using Newtonsoft.Json;

namespace PrismBarbearia.Models
{
    public class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
    }
}