using SK.VenueBooking.Model;
using SK.VenueBooking.RepositoryAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepostitory _bookRepostitory;

        public BookService(IBookRepostitory bookRepostitory)
        {
            _bookRepostitory = bookRepostitory;
        }
        public void BookVenue(BookingInfo bookInfo, string username)
        {
            _bookRepostitory.BookVenue(bookInfo, username);
        }
    }
}
