using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Model
{
    public class BookingInfo : AuditField
    {
        public Guid BookingId { get; set; }
        public string HallId { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
