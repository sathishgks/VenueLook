using Dapper;
using Microsoft.Extensions.Caching.Memory;
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
        private readonly IMemoryCache _memoryCache;

        public TenantRepository(IDatabaseWrapper databaseWrapper, IMemoryCache memoryCache)
        {
            _databaseWrapper = databaseWrapper;
            _memoryCache = memoryCache;
        }
        private async Task<List<TenantInfo>> GetTenants()
        {
            var result = _databaseWrapper.GetAll<TenantInfo>(VenueConstants.GetTenantInfo, null, CommandType.StoredProcedure);
            return await Task.FromResult(result);
        }

        private async Task<List<TenantUserMap>> GetTenantUserMap()
        {
            var result = _databaseWrapper.GetAll<TenantUserMap>(VenueConstants.GetUserTenantMap, null, CommandType.StoredProcedure);
            return await Task.FromResult<List<TenantUserMap>>(result);
        }

        public async Task LoadTenantUserCache()
        {
            var tenantinfo = await GetTenants();
            if (_memoryCache?.Get(VenueConstants.TenantCache) != null)
                _memoryCache.Set<List<TenantInfo>>(VenueConstants.TenantCache, tenantinfo);
            var tenantusermap = await GetTenantUserMap();
            if (_memoryCache?.Get(VenueConstants.UserTenantCache) != null)
                _memoryCache.Set<List<TenantUserMap>>(VenueConstants.UserTenantCache, tenantusermap);
        }
    }
}
