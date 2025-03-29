using EmployeeUnitManagementDomain.Models.Responses;

namespace EmployeeUnitManagementDomain.Interfaces.Service
{
    public interface IBaseService<T> where T : class
    {
        Task<GenericResponse> Create(T entity);
        Task<List<T>> SelectAll(T entity);
    }
}
