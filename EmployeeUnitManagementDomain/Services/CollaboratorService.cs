using EmployeeUnitManagementDomain.Interfaces.Repository;
using EmployeeUnitManagementDomain.Interfaces.Service;
using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;

namespace EmployeeUnitManagementDomain.Services
{
    public class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IUnityService _unityService;
        public CollaboratorService(ICollaboratorRepository collaboratorRepository, IUnityService unityService)
        {
            _collaboratorRepository = collaboratorRepository;
            _unityService = unityService;
        }

        public async Task<GenericResponse> Create(Collaborator entity)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                if (await _unityService.UnityIsActive(entity.UnityId))
                {
                    response.IsSuccess = await _collaboratorRepository.Create(entity);
                    response.Message = response.IsSuccess ? "Colaborador cadastrada com sucesso" : "Não foi possível cadastrar o colaborador";
                }
                else
                {
                    response.Message = "Unidade não está ativa";
                }
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Ocorreu um erro no cadastro do colaborador";
            }

            return response;
        }

        public async Task<GenericResponse> Delete(Collaborator entity)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                response.IsSuccess = await _collaboratorRepository.Delete(entity);
                response.Message = response.IsSuccess ? "Colaborador removido com sucesso" : "Não foi possível remover o colaborador";
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Ocorreu um erro na remo~ção do colaborador";
            }

            return response;
        }

        public async Task<List<Collaborator>> SelectAll(Collaborator entity)
        {
            try
            {
                return await _collaboratorRepository.SelectAll(entity);
            }
            catch (Exception)
            {
                return new List<Collaborator>();
            }
        }

        public async Task<GenericResponse> Update(CollaboratorUpdateRequest request)
        {
            GenericResponse response = new GenericResponse();
            try
            {
                if (await _unityService.UnityIsActive(request.UnityId))
                {
                    response.IsSuccess = await _collaboratorRepository.Update(request);
                    response.Message = response.IsSuccess ? "Colaborador alterado com sucesso" : "Não foi possível alterar o colaborador";
                }
                else
                {
                    response.Message = "Unidade não está ativa";
                }
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.Message = "Ocorreu um erro na alteração do colaborador";
            }

            return response;
        }
    }
}
