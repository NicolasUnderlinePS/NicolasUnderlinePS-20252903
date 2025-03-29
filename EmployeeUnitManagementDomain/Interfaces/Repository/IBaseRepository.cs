namespace EmployeeUnitManagementDomain.Interfaces.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<bool> Create(T entity);
        Task<List<T>> SelectAll(T entity);
    }
}
