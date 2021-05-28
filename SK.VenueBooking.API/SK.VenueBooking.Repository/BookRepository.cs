using Dapper;
using SK.VenueBooking.Misc;
using SK.VenueBooking.Model;
using SK.VenueBooking.ORM;
using SK.VenueBooking.RepositoryAbstraction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Repository
{
    public class BookRepository : IBookRepostitory
    {
        private readonly IDatabaseWrapper _databaseWrapper;

        public BookRepository(IDatabaseWrapper databaseWrapper)
        {
            _databaseWrapper = databaseWrapper;
        }
        public void BookVenue(BookingInfo bookInfo,string username)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add(VenueConstants.HallId, bookInfo.HallId);
            dbPara.Add(VenueConstants.BookingDate, bookInfo.BookingDate);
            dbPara.Add(VenueConstants.CreatedBy, username);
            _databaseWrapper.Insert<VenueInfo>(VenueConstants.InsertHallInfo, dbPara, false, CommandType.StoredProcedure);
        }

    }
}
