using System;
using Xamarin.Forms;

namespace PrismBarbearia.Models
{
    public class BarberShopAppointment //: ScheduleAppointment
    {
        public string EventName { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Color Color { get; set; }
    }
}