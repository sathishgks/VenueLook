using SK.VenueBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.RepositoryAbstraction
{
    public interface IVenueRepository
    {
        public List<VenueInfo> GetVenueDetails();
        public List<BookingInfo> GetBookingetails();
    }
}
