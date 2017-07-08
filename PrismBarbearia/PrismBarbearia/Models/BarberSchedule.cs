using Microsoft.WindowsAzure.MobileServices;
using System;

namespace PrismBarbearia.Models
{
    [DataTable("agendamentos")]
    public class BarberSchedule
    {
        public string Id { get; set; }
        public string FacebookID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Service { get; set; }
        public string Date { get; set; }
        public string Hour { get; set; }
        public DateTime DateTime { get; set; }
    }
}
