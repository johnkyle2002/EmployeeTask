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
                    EmployeeNumber = s.EmployeeNumber
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
    }
}
