using Microsoft.WindowsAzure.MobileServices;
using System;
using Prism.Navigation;

namespace PrismBarbearia.Models
{
    [DataTable("agendamentos")]
    public class BarberService
    {
        public string id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDetail { get; set; }
        public string ServiceImage { get; set; }
        public string servicePrice;
        public string ServicePrice {
            get
            {
                return servicePrice;
            }
            set
            {
                servicePrice = "R$ " + value;
            }
        }
        public string version { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public bool deleted { get; set; }

        public static implicit operator BarberService(NavigationParameters v)
        {
            throw new NotImplementedException();
        }
    }
}