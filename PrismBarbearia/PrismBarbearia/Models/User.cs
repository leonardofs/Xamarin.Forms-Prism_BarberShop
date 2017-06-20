using Newtonsoft.Json;

namespace PrismBarbearia.Models
{
    class User
    {
        [JsonProperty("userId")]
        public string UserId { get; set; }
    }
}