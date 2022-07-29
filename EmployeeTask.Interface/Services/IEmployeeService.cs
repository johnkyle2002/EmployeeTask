using EmployeeTask.Shared.DataTrasferObject;

namespace EmployeeTask.Interface.Services
{
    public interface IEmployeeService
    {
        Task<IOperationResult<int?>> Create(EmployeeDTO employee);
        Task<IOperationResult<EmployeeDTO>> Get(int empNumber);
        Task<IOperationResult<IList<EmployeeDTO>>> GetAll();
        Task<IOperationResult> Remove(int empNumber);
        Task<IOperationResult> Update(EmployeeDTO employee);
    }
}
