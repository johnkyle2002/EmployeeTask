using EmployeeTask.Shared.DataTrasferObject;

namespace EmployeeTask.Interface.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IOperationResult<int?>> Create(EmployeeDTO employee);
        Task<IOperationResult> Remove(int empNumber);
        Task<IOperationResult> Update(EmployeeDTO employee);
    }
}
