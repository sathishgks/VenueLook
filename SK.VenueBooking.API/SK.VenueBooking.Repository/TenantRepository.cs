using SK.VenueBooking.RepositoryAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Repository
{
   public class TenantRepository : ITenantRepository
    {
        public List<string> GetTenants(string username)
        {
            throw new NotImplementedException();
        }
    }
}
