using Dapper;
using Microsoft.AspNetCore.Http;
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
        

        public TenantRepository(IDatabaseWrapper databaseWrapper)
        {
            _databaseWrapper = databaseWrapper;
        }
        private List<TenantInfo> GetTenants()
        {
            var result = _databaseWrapper.GetAll<TenantInfo>(VenueConstants.GetTenantInfo, null,true, CommandType.StoredProcedure);
            return result;
        }

        private List<TenantUserMap> GetTenantUserMap()
        {
            var result = _databaseWrapper.GetAll<TenantUserMap>(VenueConstants.GetUserTenantMap, null,true, CommandType.StoredProcedure);
            return result;
        }

        public void LoadTenantUserCache()
        {
            var tenantusermap = GetTenantUserMap();
            var tenantinfo = GetTenants();
           
            if (!DataStore.datastorecollection.ContainsKey(VenueConstants.TenantCache))
            { DataStore.datastorecollection.Add(VenueConstants.TenantCache, tenantinfo); }
            if (!DataStore.datastorecollection.ContainsKey(VenueConstants.UserTenantCache))
            { DataStore.datastorecollection.Add(VenueConstants.UserTenantCache, tenantusermap); }
        }
    }
}
