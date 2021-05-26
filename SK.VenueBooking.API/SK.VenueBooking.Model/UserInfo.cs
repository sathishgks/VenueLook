using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Model
{
    public class UserInfo : AuditField
    {
       public Guid UserId { get; set; }
       public string UserName { get; set; }
      //  List<string> TenantName;
    }
}
