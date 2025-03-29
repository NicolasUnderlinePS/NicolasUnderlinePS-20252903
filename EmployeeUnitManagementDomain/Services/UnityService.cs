using EmployeeUnitManagementDomain.Interfaces.Repository;
using EmployeeUnitManagementDomain.Interfaces.Service;
using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;

namespace EmployeeUnitManagementDomain.Services
{
    public class UnityService : IUnityService
    {
        private readonly IUnityRepository _unityRepository;

        public UnityService(IUnityRepository unityRepository)
        {
            _unityRepository = unityRepository;
        }

        public async Task<GenericResponse> Create(Unity entity)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                response.IsSuccess = await _unityRepository.Create(entity);

                response.Message = response.IsSuccess ? "Unidade cadastrada com sucesso" : "Não foi possível cadastrar a unidade";
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Ocorreu um erro no cadastro da unidade";
            }

            return response;
        }

        public async Task<List<Unity>> SelectAll(Unity entity)
        {
            try
            {
                return await _unityRepository.SelectAll(entity);
            }
            catch (Exception)
            {
                return new List<Unity>();
            }
        }

        public async Task<List<CollaboratorAssociatedResponse>> SelectCollaboratorAssociated(Unity entity)
        {
            try
            {
                return await _unityRepository.SelectCollaboratorAssociated();
            }
            catch (Exception)
            {
                return new List<CollaboratorAssociatedResponse>();
            }
        }

        public async Task<bool> UnityIsActive(int unityId)
        {
            try
            {
                return await _unityRepository.IsActive(unityId);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<GenericResponse> Update(UnityUpdateRequest request)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                response.IsSuccess = await _unityRepository.Update(request);

                response.Message = response.IsSuccess ? "Unidade alterada com sucesso" : "Não foi possível alterar a unidade";
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Ocorreu um erro ao alterar unidade";
            }

            return response;
        }
    }
}
