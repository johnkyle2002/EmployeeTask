using EmployeeTask.Interface.Repositories;
using EmployeeTask.Interface.Services;
using EmployeeTask.Shared.DataTrasferObject;
using EmployeeTask.Shared.Enumerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeTask.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IQueryableRepository _query;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository repository,
            IQueryableRepository query,
            ILogger<EmployeeService> logger)
        {
            _repository = repository;
            _query = query;
            _logger = logger;
        }

        public async Task<IOperationResult<int?>> Create(EmployeeDTO employee)
        {
            return await _repository.Create(employee);
        }

        public async Task<IOperationResult<EmployeeDTO>> Get(int empNumber)
        {
            try
            {
                var result = await _query.EmpoyeeQuery()
                    .Where(w => w.EmployeeNumber == empNumber)
                    .Select(s => new EmployeeDTO
                    {
                        FirstName = s.FirstName,
                        Lastname = s.Lastname,
                        EmployeeNumber = s.EmployeeNumber
                    }).FirstOrDefaultAsync();

                if (result is null)
                    return new OperationResult<EmployeeDTO>
                    {
                        StatusCode = StatusCodeEnum.Code.BadRequest,
                        Message = "Not found."
                    };

                return new OperationResult<EmployeeDTO>
                {
                    Entity = result,
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeService.Get");

                return new OperationResult<EmployeeDTO>
                {
                    Message = "Error found.",
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
        }

        public async Task<IOperationResult<IList<EmployeeDTO>>> GetAll()
        {
            try
            {
                var result = await _query.EmpoyeeQuery().Select(s => new EmployeeDTO
                {
                    FirstName = s.FirstName,
                    Lastname = s.Lastname,
                    EmployeeNumber = s.EmployeeNumber,
                    Temperature = s.Temperature,
                }).ToListAsync();

                return new OperationResult<IList<EmployeeDTO>>
                {
                    Entity = result,
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeService.GetAll");

                return new OperationResult<IList<EmployeeDTO>>
                {
                    Message = "Error found.",
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
        }

        public async Task<IOperationResult> Remove(int empNumber)
        {
            return await _repository.Remove(empNumber);
        }

        public async Task<IOperationResult> Update(EmployeeDTO employee)
        {
            return await _repository.Update(employee);
        }

        public async Task<IOperationResult<IList<EmployeeDTO>>> GetAllByFilter(IList<EmployeeFilterDTO> filters)
        {
            if (filters is null || !filters.Any())
                return new OperationResult<IList<EmployeeDTO>>
                {
                    StatusCode = StatusCodeEnum.Code.Ok,
                    Entity = await _query.EmpoyeeQuery().ToListAsync()
                };

            try
            {
                var employee = _query.EmpoyeeQuery();
                foreach (var item in filters)
                {
                    switch (item.FilterBy)
                    {
                        case EmployeeFilterEnum.Employee.FirstName:
                            employee = employee.Where(w => EF.Functions.Like(w.FirstName, $"%{item.FilterValue}%"));
                            break;
                        case EmployeeFilterEnum.Employee.LastName:
                            employee = employee.Where(w => EF.Functions.Like(w.Lastname, $"%{item.FilterValue}%"));
                            break;
                        case EmployeeFilterEnum.Employee.Temperature:
                            employee = employee.Where(w => w.Temperature.Any(a=> a.Temperature >= item.StartDecimal && a.Temperature <= item.EndDecimal));
                            break;
                        case EmployeeFilterEnum.Employee.RecordDate:
                            employee = employee.Where(w => w.Temperature.Any(a => a.RecordDate >= item.StartDate && a.RecordDate <= item.EndDate));
                            break;
                    }
                }

                return new OperationResult<IList<EmployeeDTO>>
                {
                    StatusCode = StatusCodeEnum.Code.Ok,
                    Entity = await employee.ToListAsync()
                };

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeService.GetAllByFilter");

                return new OperationResult<IList<EmployeeDTO>>
                {
                    Message = "Error found.",
                    StatusCode = StatusCodeEnum.Code.InternalError,
                };
            }

        }
    }
}
