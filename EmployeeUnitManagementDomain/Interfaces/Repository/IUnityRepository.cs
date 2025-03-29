using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;

namespace EmployeeUnitManagementDomain.Interfaces.Repository
{
    public interface IUnityRepository : IBaseRepository<Unity>
    {
        Task<bool> IsActive(int unityId);
        Task<bool> Update(UnityUpdateRequest request);
        Task<List<CollaboratorAssociatedResponse>> SelectCollaboratorAssociated();
    }
}
