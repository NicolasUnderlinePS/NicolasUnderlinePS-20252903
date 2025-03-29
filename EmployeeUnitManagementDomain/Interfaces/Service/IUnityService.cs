using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;

namespace EmployeeUnitManagementDomain.Interfaces.Service
{
    public interface IUnityService : IBaseService<Unity>
    {
        Task<GenericResponse> Update(UnityUpdateRequest request);
        Task<bool> UnityIsActive (int unityId);
        Task<List<CollaboratorAssociatedResponse>> SelectCollaboratorAssociated(Unity entity);
    }
}
