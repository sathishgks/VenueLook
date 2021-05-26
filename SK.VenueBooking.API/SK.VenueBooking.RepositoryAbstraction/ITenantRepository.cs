using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.RepositoryAbstraction
{
   public interface ITenantRepository
    {
        public List<string> GetTenants(string username);
    }
}
