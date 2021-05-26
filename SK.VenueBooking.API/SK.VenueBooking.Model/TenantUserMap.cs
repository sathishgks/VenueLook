using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Model
{
   public class TenantUserMap
    {
        public string TenantName { get; set; }
        public string UserName { get; set; }

        public int RoleId { get; set; }
    }
}
