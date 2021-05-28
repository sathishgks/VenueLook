using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SK.VenueBooking.API.MiddleWare
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
    }
}
