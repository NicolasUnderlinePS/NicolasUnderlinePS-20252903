using EmployeeUnitManagementDomain.Models.Entities;
using EmployeeUnitManagementDomain.Models.Requests;

namespace EmployeeUnitManagementDomain.Interfaces.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> Update(UserUpdateRequest request); 
    }
}
