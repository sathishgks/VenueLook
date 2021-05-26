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
   public class TenantRepository : ITenantRepository
    {
        private readonly IDatabaseWrapper _databaseWrapper;
        public TenantRepository(IDatabaseWrapper databaseWrapper)
        {
            _databaseWrapper = databaseWrapper;
        }
        public async Task<List<TenantInfo>> GetTenants()
        {
            var result = _databaseWrapper.Get<List<TenantInfo>>(VenueConstants.GetTenantInfo, null, CommandType.StoredProcedure);
            return await Task.FromResult<List<TenantInfo>>(result);
        }

        public async Task<List<TenantUserMap>> GetTenantUserMap()
        {
            var result = _databaseWrapper.Get<List<TenantUserMap>>(VenueConstants.GetUserTenantMap, null, CommandType.StoredProcedure);
            return await Task.FromResult<List<TenantUserMap>>(result);
        }
    }
}
