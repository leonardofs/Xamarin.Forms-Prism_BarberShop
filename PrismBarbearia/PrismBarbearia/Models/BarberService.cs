using Microsoft.WindowsAzure.MobileServices;
using System;

namespace PrismBarbearia.Models
{
    [DataTable("agendamentos")]
    public class BarberService
    {
        public string Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDetail { get; set; }
        public string ServiceImage { get; set; }     
        public string ServicePrice { get; set; }
    }
}