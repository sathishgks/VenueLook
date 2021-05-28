using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace SK.VenueBooking.ORM
{
    public interface IDatabaseWrapper : IDisposable
    {
        DbConnection GetDbconnection();
        T Get<T>(string sp, DynamicParameters parms, bool isCatalog = false, CommandType commandType = CommandType.StoredProcedure);
        List<T> GetAll<T>(string sp, DynamicParameters parms, bool isCatalog = false, CommandType commandType = CommandType.StoredProcedure);
        int Execute(string sp, DynamicParameters parms, bool isCatalog = false, CommandType commandType = CommandType.StoredProcedure);
        T Insert<T>(string sp, DynamicParameters parms, bool isCatalog = false, CommandType commandType = CommandType.StoredProcedure);
        T Update<T>(string sp, DynamicParameters parms, bool isCatalog = false, CommandType commandType = CommandType.StoredProcedure);
    }
}
