using SK.VenueBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.RepositoryAbstraction
{
    public interface IBookRepostitory
    {
        public void BookVenue(BookingInfo bookInfo,string username);
        
    }
}
