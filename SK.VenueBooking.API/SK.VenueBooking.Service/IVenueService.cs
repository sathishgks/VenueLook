using SK.VenueBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Service
{
    public interface IVenueService
    {
        public void AddVenue(VenueInfo venueInfo, string username);
        public Task<List<VenueInfo>> GetVenueDetails();
    }
}
