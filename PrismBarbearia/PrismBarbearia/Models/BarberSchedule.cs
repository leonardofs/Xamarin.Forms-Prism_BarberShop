using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismBarbearia.Models
{
    public class BarberSchedule
    {
            public string id { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime updatedAt { get; set; }
            public string version { get; set; }
            public bool deleted { get; set; }
            public string FacebookID { get; set; }
            public string Hour { get; set; }
            public string Service { get; set; }
            public string Day { get; set; }
    }
}
