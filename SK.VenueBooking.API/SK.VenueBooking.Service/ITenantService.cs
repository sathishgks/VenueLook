using SK.VenueBooking.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Service
{
   public interface ITenantService
    {
        public Task<List<TenantInfo>> GetTenants();
        public Task<List<TenantUserMap>> GetTenantUserMap();
    }
}
