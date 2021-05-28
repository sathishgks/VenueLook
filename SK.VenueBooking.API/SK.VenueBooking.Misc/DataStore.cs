using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Misc
{
    public static class DataStore
    {
        // Need to implement cache Mechanism like Redis
        public static Dictionary<string, object> datastorecollection = new Dictionary<string, object>();
    }
}
