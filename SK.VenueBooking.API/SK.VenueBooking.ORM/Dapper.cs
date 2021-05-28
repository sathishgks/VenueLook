using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using SK.VenueBooking.Misc;
using SK.VenueBooking.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace SK.VenueBooking.ORM
{
    public class CustomDapper : IDatabaseWrapper
    {
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomDapper(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _config = config;
            _httpContextAccessor = httpContextAccessor;
        }
      
        public T Get<T>(string sp, DynamicParameters parms, bool isCatalog = false, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(GetConnectionString(isCatalog));
            return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
        }

        private string GetConnectionString(bool isCatalog = false)
        {

            // Need to Refactor
            string connstring = String.Empty;
            var username = getusername();
            if (DataStore.datastorecollection != null && DataStore.datastorecollection.Count > 0 && DataStore.datastorecollection.ContainsKey(VenueConstants.UserTenantCache)&& !string.IsNullOrWhiteSpace(username))
            {
                var map = DataStore.datastorecollection[VenueConstants.UserTenantCache] as List<TenantUserMap>;
                TenantInfo tenantInfo = new TenantInfo();
               // TenantUserMap usermap = new TenantUserMap();
               var usermap = map?.Where(x => x.UserName == username).FirstOrDefault();
                if(usermap.RoleId==1)
                {
                    connstring = (isCatalog) ? _config.GetConnectionString(VenueConstants.Connectionstring) : GetAdminUserTenant(username);
                }
                else
                {
                    var tenantinfo = DataStore.datastorecollection[VenueConstants.TenantCache] as List<TenantInfo>;
                    var tenant = tenantinfo?.Where(x => x.TenantName == usermap.TenantName).FirstOrDefault();

                  //  tenantInfo = DataStore.datastorecollection[usermap.TenantName] as TenantInfo;
                    connstring = tenant.Connection;
                }
            }
            return (!string.IsNullOrWhiteSpace(connstring)) ? connstring : _config.GetConnectionString(VenueConstants.Connectionstring);

        }

        private string GetAdminUserTenant(string username)
        {
           // Need to refactor
            TenantInfo tenantInfo = new TenantInfo();
            var runTimeTenant = DataStore.datastorecollection[VenueConstants.AdminRunTimeTenant] as List<RunTimeTenant>;
            var rTenant = runTimeTenant?.Where(x => x.UserName == username) as RunTimeTenant;
            if (rTenant != null)
            {
                tenantInfo = DataStore.datastorecollection[rTenant?.Tenant] as TenantInfo;
            }
            return tenantInfo.Connection;

        }

        private string getusername()
        {
            return (DataStore.datastorecollection.ContainsKey(VenueConstants.Loggedinuser))? DataStore.datastorecollection[VenueConstants.Loggedinuser].ToString():string.Empty;
        }
        public List<T> GetAll<T>(string sp, DynamicParameters parms, bool isCatalog = false, CommandType commandType = CommandType.StoredProcedure)
        {
            using IDbConnection db = new SqlConnection(GetConnectionString());
            return db.Query<T>(sp, parms, commandType: commandType).ToList();
        }

        public DbConnection GetDbconnection()
        {
            return new SqlConnection(GetConnectionString());

        }

        public T Insert<T>(string sp, DynamicParameters parms, bool isCatalog = false, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(GetConnectionString());
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public void Dispose()
        {

        }

        public int Execute(string sp, DynamicParameters parms, bool isCatalog = false,CommandType commandType = CommandType.StoredProcedure)
        {
            throw new NotImplementedException();
        }

        public T Update<T>(string sp, DynamicParameters parms, bool isCatalog = false, CommandType commandType = CommandType.StoredProcedure)
        {
            T result;
            using IDbConnection db = new SqlConnection(GetConnectionString());
            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, commandType: commandType, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
    }
}
