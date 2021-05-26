using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Model
{
    public class TenantInfo : AuditField
    {
        public Guid TenantId { get; set; }
        public string TenantName { get; set; }
        public string TenantConnection { get; set; }

    }
}
