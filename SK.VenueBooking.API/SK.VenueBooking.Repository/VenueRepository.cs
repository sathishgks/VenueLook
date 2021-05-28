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
    public class VenueRepository : IVenueRepository
    {
        private readonly IDatabaseWrapper _databaseWrapper;

        public VenueRepository(IDatabaseWrapper databaseWrapper)
        {
            _databaseWrapper = databaseWrapper;
        }
        public void AddVenue(VenueInfo venueInfo,string username)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add(VenueConstants.HallName, venueInfo.HallName);
            dbPara.Add(VenueConstants.LocationInfo, venueInfo.Location);
            dbPara.Add(VenueConstants.Price, venueInfo.Price);
            dbPara.Add(VenueConstants.CreatedBy,username);
            _databaseWrapper.Insert<VenueInfo>(VenueConstants.InsertHallInfo, dbPara, false,CommandType.StoredProcedure);
        }

        public async Task<List<VenueInfo>> GetVenueDetails()
        {
            var result = _databaseWrapper.GetAll<VenueInfo>(VenueConstants.GetHallInfo, null, false, CommandType.StoredProcedure);
            return await Task.FromResult(result);
        }
    }
}
