using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;

namespace EmployeeUnitManagementDomain.Interfaces.Repository
{
    public interface ICollaboratorRepository : IBaseRepository<Collaborator>
    {
        Task<bool> Update(CollaboratorUpdateRequest request);
        Task<bool> Delete(Collaborator entity);
    }
}
