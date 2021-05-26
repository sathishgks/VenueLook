using SK.VenueBooking.Model;
using SK.VenueBooking.RepositoryAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Repository
{
    public class VenueRepository : IVenueRepository
    {
        public async Task<List<BookingInfo>> GetBookingetails()
        {
            throw new NotImplementedException();
        }

        public async Task<List<VenueInfo>> GetVenueDetails()
        {
            throw new NotImplementedException();
        }
    }
}
