using SK.VenueBooking.Model;
using SK.VenueBooking.RepositoryAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Service
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;

        public VenueService(IVenueRepository venueRepository)
        {
            _venueRepository = venueRepository;
        }
        public void AddVenue(VenueInfo venueInfo, string username)
        {
            _venueRepository.AddVenue(venueInfo, username);
        }

        public Task<List<VenueInfo>> GetVenueDetails()
        {
            return _venueRepository.GetVenueDetails();
        }
    }
}
