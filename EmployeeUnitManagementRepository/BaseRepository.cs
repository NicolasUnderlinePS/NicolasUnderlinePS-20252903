using EmployeeUnitManagementDomain.Interfaces.Repository;
using Npgsql;
using System.Data;

namespace EmployeeUnitManagementRepository
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;

        public BaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        internal IDbConnection GetConnection() {
            return new NpgsqlConnection(_connectionString);
        }

    }
}
