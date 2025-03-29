using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;
using EmployeeUnitManagementDomain.Models.Responses;

namespace EmployeeUnitManagementDomain.Interfaces.Service
{
    public interface IUserService : IBaseService<User>
    {
        Task<GenericResponse> Update(UserUpdateRequest request);
    }
}
