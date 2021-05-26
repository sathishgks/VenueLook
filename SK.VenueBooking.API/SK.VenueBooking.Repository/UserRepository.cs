using Dapper;
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
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseWrapper _databaseWrapper;

        public UserRepository(IDatabaseWrapper databaseWrapper)
        {
            _databaseWrapper = databaseWrapper;
        }
        public async Task<UserInfo> GetUserInfo(string username)
        {
            var dbPara = new DynamicParameters();
            dbPara.Add("@UserName", username);
            var result = _databaseWrapper.Get<UserInfo>("GetUserInfo",dbPara,CommandType.StoredProcedure);
            return await Task.FromResult<UserInfo>(result);
        }

    }
}
