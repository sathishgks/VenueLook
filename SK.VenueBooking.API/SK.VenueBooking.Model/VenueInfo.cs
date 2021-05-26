using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Model
{
    public class VenueInfo : AuditField
    {
        public Guid HallId { get; set; }
        public string HallName { get; set; }
        public string Location { get; set; }
        public string Price { get; set; }

        public string TenantName { get; set; }
    }
}
