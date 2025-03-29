using Dapper;
using EmployeeUnitManagementDomain.Interfaces.Repository;
using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Utils;
using Npgsql;
using System.Data;
using System.Text;

namespace EmployeeUnitManagementRepository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString){}

        public async Task<bool> Create(User entity)
        {
            StringBuilder query = new StringBuilder();

            try
            {
                query.AppendLine(@"
                    INSERT INTO ""User"" (Login, Password, StatusActive) 
                    VALUES (@Login, @Password, @StatusActive)
                ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Login", entity.Login, DbType.AnsiString, ParameterDirection.Input, User.MaxLengthLogin);
                parameters.Add("@Password", PasswordHash.TransformIntoSha256(entity.Password), DbType.AnsiString, ParameterDirection.Input, User.MaxLengthPassword);
                parameters.Add("@StatusActive", entity.StatusActive, DbType.Boolean, ParameterDirection.Input);

                using IDbConnection _connection = GetConnection();

                return await _connection.ExecuteAsync(query.ToString(), parameters) > 0;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao inserir usuário: {ex.Message}");
                return false;
            }
        }

        public async Task<List<User>> SelectAll(User entity)
        {
            StringBuilder query = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();

            try
            {
                query.AppendLine(@"
                    SELECT *
                    FROM ""User""
                ");


                if (entity.StatusActive != null)
                {
                    query.AppendLine(@"
                        WHERE StatusActive = @StatusActive
                    ");

                    parameters.Add("@StatusActive", entity.StatusActive, DbType.Boolean, ParameterDirection.Input);
                }

                using IDbConnection _connection = GetConnection();

                return (await _connection.QueryAsync<User>(query.ToString(), parameters)).AsList();
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao consultar usuários: {ex.Message}");
                return new List<User>();
            }
        }

        public async Task<bool> Update(UserUpdateRequest request)
        {
            StringBuilder query = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            List<string> setClauses = new List<string>();

            try
            {
                query.AppendLine(@"
                    UPDATE ""User"" 
                    SET
                ");

                setClauses.Add("StatusActive  = @StatusActive");
                parameters.Add("@StatusActive", request.StatusActive, DbType.Boolean, ParameterDirection.Input);

                if (string.IsNullOrWhiteSpace(request.Password) == false)
                {
                    setClauses.Add("Password = @Password");
                    parameters.Add("@Password", PasswordHash.TransformIntoSha256(request.Password), DbType.String, ParameterDirection.Input, User.MaxLengthPassword);
                }

                query.Append(string.Join(", ", setClauses));
                query.Append(" WHERE Id = @Id");

                parameters.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                
                using IDbConnection _connection = GetConnection();

                return await _connection.ExecuteAsync(query.ToString(), parameters) > 0;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao alterar usuário: {ex.Message}");
                return false;
            }
        }
    }
}
