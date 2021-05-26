using SK.VenueBooking.Model;
using SK.VenueBooking.RepositoryAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SK.VenueBooking.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserInfo> GetUserInfo(string username)
        {
           return await _userRepository?.GetUserInfo(username);
        }
    }
}
