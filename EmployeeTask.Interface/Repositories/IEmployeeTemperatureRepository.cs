using EmployeeTask.Shared.DataTrasferObject;

namespace EmployeeTask.Interface.Repositories
{
    public interface IEmployeeTemperatureRepository
    {
        Task<IOperationResult<int?>> Create(EmployeeTemperatureDTO temperature); 
        Task<IOperationResult> Remove(int tempID);
        Task<IOperationResult> Update(EmployeeTemperatureDTO temperature);
    }
}
