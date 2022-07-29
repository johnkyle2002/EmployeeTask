using EmployeeTask.Shared.DataTrasferObject;

namespace EmployeeTask.Interface.Services
{
    public interface IEmployeeTemperatureService
    {
        Task<IOperationResult<int?>> Create(EmployeeTemperatureDTO temperature);
        Task<IOperationResult<EmployeeTemperatureDTO>> Get(int tempID);
        Task<IOperationResult<IList<EmployeeTemperatureDTO>>> GetAll();
        Task<IOperationResult> Remove(int tempID);
        Task<IOperationResult> Update(EmployeeTemperatureDTO temperature);
    }
}
