using Dapper;
using EmployeeUnitManagementDomain.Interfaces.Repository;
using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Utils;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Text;
using static Dapper.SqlMapper;

namespace EmployeeUnitManagementRepository
{
    public class CollaboratorRepository : BaseRepository, ICollaboratorRepository
    {
        public CollaboratorRepository(string connectionString) : base(connectionString){}

        public async Task<bool> Create(Collaborator entity)
        {
            StringBuilder query = new StringBuilder();

            try
            {
                query.AppendLine(@"
                    INSERT INTO Collaborator (FullName, UserId, UnityId) 
                    VALUES (@FullName, @UserId, @UnityId)
                ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@FullName", entity.FullName, DbType.AnsiString, ParameterDirection.Input, Collaborator.MaxLengthFullName);
                parameters.Add("@UserId", entity.UserId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@UnityId", entity.UnityId, DbType.Int32, ParameterDirection.Input);

                using IDbConnection _connection = GetConnection();

                return await _connection.ExecuteAsync(query.ToString(), parameters) > 0;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao inserir colaborador: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(Collaborator entity)
        {
            StringBuilder query = new StringBuilder();

            try
            {
                query.AppendLine(@"
                    DELETE FROM Collaborator WHERE Id = @Id
                ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", entity.Id, DbType.Int32, ParameterDirection.Input);

                using IDbConnection _connection = GetConnection();

                return await _connection.ExecuteAsync(query.ToString(), parameters) > 0;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao deletar colaborador: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Collaborator>> SelectAll(Collaborator entity)
        {
            StringBuilder query = new StringBuilder();

            try
            {
                query.AppendLine(@"
                    SELECT *
                    FROM Collaborator
                ");

                using IDbConnection _connection = GetConnection();

                return (await _connection.QueryAsync<Collaborator>(query.ToString())).AsList();
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao consultar colaboradores: {ex.Message}");
                return new List<Collaborator>();
            }
        }

        public async Task<bool> Update(CollaboratorUpdateRequest request)
        {
            StringBuilder query = new StringBuilder();
            DynamicParameters parameters = new DynamicParameters();
            List<string> setClauses = new List<string>();

            try
            {
                query.AppendLine(@"
                    UPDATE Collaborator 
                    SET
                ");

                if (string.IsNullOrWhiteSpace(request.FullName) == false)
                {
                    setClauses.Add("FullName = @FullName");
                    parameters.Add("@FullName", request.FullName, DbType.AnsiString, ParameterDirection.Input, Collaborator.MaxLengthFullName);
                }

                if (request.UnityId > 0)
                {
                    setClauses.Add("UnityId = @UnityId");
                    parameters.Add("@UnityId", request.UnityId, DbType.Int32, ParameterDirection.Input);
                }

                query.Append(string.Join(", ", setClauses));
                query.Append(" WHERE Id = @Id");
                parameters.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);

                if (setClauses.Any() == false)
                    return false;

                using IDbConnection _connection = GetConnection();

                return await _connection.ExecuteAsync(query.ToString(), parameters) > 0;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao alterar colaborador: {ex.Message}");
                return false;
            }
        }
    }
}
