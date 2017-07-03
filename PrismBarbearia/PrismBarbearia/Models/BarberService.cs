using Microsoft.WindowsAzure.MobileServices;

namespace PrismBarbearia.Models
{
    [DataTable("agendamentos")]
    public class BarberService
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Version]
        public string AzureVersion { get; set; }

        public string Name { get; set; }

        private string price;

        public string Price
        {
            get { return price; }
            set { price = "R$ " + value; }
        }


        public string Image { get; set; }
    }
}