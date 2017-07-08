using Microsoft.WindowsAzure.MobileServices;
using System;

namespace PrismBarbearia.Models
{
    public class BarberService
    {
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDetail { get; set; }
        public string ServiceImage { get; set; }    

        private string servicePrice;
        public string ServicePrice
        {
            get { return "R$ "+servicePrice; }
            set { servicePrice = value; }
        }
    }
}