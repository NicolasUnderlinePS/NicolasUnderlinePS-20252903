using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;

namespace EmployeeUnitManagementDomain.Interfaces.Service
{
    public interface ICollaboratorService : IBaseService<Collaborator>
    {
        Task<GenericResponse> Update(CollaboratorUpdateRequest request);
        Task<GenericResponse> Delete(Collaborator entity);
    }
}
