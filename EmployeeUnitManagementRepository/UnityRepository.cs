using Dapper;
using EmployeeUnitManagementDomain.Interfaces.Repository;
using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;
using EmployeeUnitManagementDomain.Models.Utils;
using Npgsql;
using System.Data;
using System.Text;

namespace EmployeeUnitManagementRepository
{
    public class UnityRepository : BaseRepository, IUnityRepository
    {
        public UnityRepository(string connectionString) : base(connectionString){}

        public async Task<bool> Create(Unity entity)
        {
            StringBuilder query = new StringBuilder();

            try
            {
                query.AppendLine(@"
                    INSERT INTO Unity (Code, Name, StatusActive) 
                    VALUES (@Code, @Name, @StatusActive)
                ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Code", entity.Code, DbType.AnsiString, ParameterDirection.Input, Unity.MaxLengthCode);
                parameters.Add("@Name", entity.Name, DbType.AnsiString, ParameterDirection.Input, Unity.MaxLengthName);
                parameters.Add("@StatusActive", entity.StatusActive, DbType.Boolean, ParameterDirection.Input);

                using IDbConnection _connection = GetConnection();

                return await _connection.ExecuteAsync(query.ToString(), parameters) > 0;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao inserir unidade: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> IsActive(int unityId)
        {
            StringBuilder query = new StringBuilder();

            try
            {
                query.AppendLine(@"
                    SELECT *
                    FROM Unity 
                    WHERE (Id = @Id)
                    AND (StatusActive = true)
                ");

                using IDbConnection _connection = GetConnection();
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", unityId, DbType.Int32, ParameterDirection.Input);

                List<Unity> response = (await _connection.QueryAsync<Unity>(query.ToString(), parameters)).AsList();

                return response.Count() > 0;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao consultar status da unidade: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Unity>> SelectAll(Unity entity)
        {
            StringBuilder query = new StringBuilder();

            try
            {
                query.AppendLine(@"
                    SELECT *
                    FROM Unity
                ");

                using IDbConnection _connection = GetConnection();

                return (await _connection.QueryAsync<Unity>(query.ToString())).AsList();
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao consultar unidades: {ex.Message}");
                return new List<Unity>();
            }
        }

        public async Task<List<CollaboratorAssociatedResponse>> SelectCollaboratorAssociated()
        {
            StringBuilder query = new StringBuilder();

            try
            {
                query.AppendLine(@"
                    SELECT 
                        u.Id AS UnityId,
                        u.Code, 
                        u.Name, 

                        c.Id AS CollaboratorId, 
                        c.FullName
                    FROM Unity u 
                    INNER JOIN Collaborator c ON c.UnityId = u.Id;
                ");

                using IDbConnection _connection = GetConnection();

                return (await _connection.QueryAsync<CollaboratorAssociatedResponse>(query.ToString())).AsList();
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao consultar associações: {ex.Message}");
                return new List<CollaboratorAssociatedResponse>();
            }
        }

        public async Task<bool> Update(UnityUpdateRequest request)
        {
            StringBuilder query = new StringBuilder();

            try
            {
                query.AppendLine(@"
                    UPDATE Unity 
                    SET StatusActive = @StatusActive
                    WHERE Id = @Id
                ");

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@StatusActive", request.StatusActive, DbType.Boolean, ParameterDirection.Input);

                using IDbConnection _connection = GetConnection();
                return await _connection.ExecuteAsync(query.ToString(), parameters) > 0;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Erro ao alterar unidade: {ex.Message}");
                return false;
            }
        }
    }
}
