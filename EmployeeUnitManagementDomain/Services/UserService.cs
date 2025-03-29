using EmployeeUnitManagementDomain.Interfaces.Repository;
using EmployeeUnitManagementDomain.Interfaces.Service;
using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;

namespace EmployeeUnitManagementDomain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GenericResponse> Create(User entity)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                response.IsSuccess = await _userRepository.Create(entity);

                response.Message = response.IsSuccess ? "Usuário cadastrado com sucesso" : "Não foi possível cadastrar o usuário";
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response .Message = "Ocorreu um erro no cadastro do usuário";
            }

            return response;
        }

        public async Task<List<User>> SelectAll(User entity)
        {
            try
            {
                return await _userRepository.SelectAll(entity);
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<GenericResponse> Update(UserUpdateRequest request)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                response.IsSuccess = await _userRepository.Update(request);

                response.Message = response.IsSuccess ? "Usuário alterado com sucesso" : "Não foi possível alterar o usuário";
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Ocorreu um erro ao alterar usuário";
            }

            return response;
        }
    }
}
