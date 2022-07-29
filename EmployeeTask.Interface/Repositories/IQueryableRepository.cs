using EmployeeTask.Shared.DataTrasferObject;

namespace EmployeeTask.Interface.Repositories
{
    public interface IQueryableRepository
    {
        IQueryable<EmployeeDTO> EmpoyeeQuery();
        IQueryable<EmployeeTemperatureDTO> EmpoyeeTemperatureQUery();
    }
}
