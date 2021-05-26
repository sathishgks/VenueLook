using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Model
{
    public class UserInfo : AuditField
    {
        string UserId;
        string UserName;
        List<string> TenantName;
    }
}
