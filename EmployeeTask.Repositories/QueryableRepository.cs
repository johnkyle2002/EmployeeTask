using EmployeeTask.Interface.Repositories;
using EmployeeTask.Models;
using EmployeeTask.Shared.DataTrasferObject;

namespace EmployeeTask.Repositories
{
    public class QueryableRepository : IQueryableRepository
    {
        private readonly EmployeeTaskDBContext _context;

        public QueryableRepository(EmployeeTaskDBContext context)
        {
            _context = context;
        }

        public IQueryable<EmployeeDTO> EmpoyeeQuery() => _context.Employees.Select(s => new EmployeeDTO
        {
            EmployeeNumber = s.EmployeeNumber,
            FirstName = s.FirstName,
            Lastname = s.Lastname,
            Temperature = s.Temparatures.Select(t => new EmployeeTemperatureDTO
            {
                Temperature = t.Temperature,
                EmployeeNumber = t.EmployeeNumber,
                EmployeeTemperatureID = t.EmployeeTemperatureID,
                RecordDate = t.RecordDate
            }).ToList()
        }).AsQueryable();

        public IQueryable<EmployeeTemperatureDTO> EmpoyeeTemperatureQUery() => _context.Temparatures
            .Select(s => new EmployeeTemperatureDTO
            {
                Temperature = s.Temperature,
                EmployeeNumber = s.EmployeeNumber,
                EmployeeTemperatureID = s.EmployeeTemperatureID,
                RecordDate = s.RecordDate,
            }).AsQueryable();
    }
}
