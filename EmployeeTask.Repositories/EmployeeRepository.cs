using EmployeeTask.Interface.Repositories;
using EmployeeTask.Models;
using EmployeeTask.Shared.DataTrasferObject;
using EmployeeTask.Shared.Enumerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeTask.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeTaskDBContext _context;
        private readonly ILogger<EmployeeRepository> _logger;

        public EmployeeRepository(EmployeeTaskDBContext context,
            ILogger<EmployeeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <summary>
        /// Return Employee ID only
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task<IOperationResult<int?>> Create(EmployeeDTO employee)
        {
            try
            {
                if (employee is null)
                    return new OperationResult<int?>
                    {
                        StatusCode = Shared.Enumerator.StatusCodeEnum.Code.BadRequest,
                        Message = "Employee must not be null."
                    };

                var model = new Employee
                {
                    FirstName = employee.FirstName,
                    Lastname = employee.Lastname
                };

                await _context.Employees.AddAsync(model);
                await _context.SaveChangesAsync();

                return new OperationResult<int?>
                {
                    StatusCode = Shared.Enumerator.StatusCodeEnum.Code.Ok,
                    Message = "Successfully created."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeRepository.Create");

                return new OperationResult<int?>
                {
                    StatusCode = Shared.Enumerator.StatusCodeEnum.Code.InternalError,
                    Message = "Error found."
                };
            }
        }

        public async Task<IOperationResult> Update(EmployeeDTO employee)
        {
            try
            {
                var model = _context.Employees.FirstOrDefault(e => e.EmployeeNumber == employee.EmployeeNumber);

                if (model is null)
                    return new OperationResult<EmployeeDTO>
                    {
                        Message = "Not found",
                        StatusCode = StatusCodeEnum.Code.BadRequest
                    };

                model.FirstName = employee.FirstName;
                model.Lastname = employee.Lastname;

                _context.Employees.Update(model);
                await _context.SaveChangesAsync();

                return new OperationResult<EmployeeDTO>
                {
                    StatusCode = StatusCodeEnum.Code.Ok,
                    Message = "Successfully saved!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeRepository.Update");

                return new OperationResult<EmployeeDTO>
                {
                    StatusCode = StatusCodeEnum.Code.InternalError,
                    Message = "Error found."
                };
            }
        }

        public async Task<IOperationResult> Remove(int empNumber)
        {
            try
            {
                var model = _context.Employees.FirstOrDefault(e => e.EmployeeNumber == empNumber);

                if (model is null)
                    return new OperationResult
                    {
                        StatusCode = StatusCodeEnum.Code.BadRequest,
                        Message = "Not found."
                    };

                _context.Employees.Remove(model);
                await _context.SaveChangesAsync();

                return new OperationResult
                {
                    StatusCode = StatusCodeEnum.Code.Ok,
                    Message = "Successfully removed."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeRepository.Update");

                return new OperationResult
                {
                    StatusCode = StatusCodeEnum.Code.InternalError,
                    Message = "Error found."
                };
            }
        }

        public async Task<IOperationResult<IList<EmployeeDTO>>> GetAll()
        {
            try
            {
                var result = await _context.Employees.Select(s => new EmployeeDTO
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
                _logger.LogError(ex, $"EmployeeRepository.Update");

                return new OperationResult<IList<EmployeeDTO>>
                {
                    Message = "Error found.",
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
        }

        public async Task<IOperationResult<EmployeeDTO>> Get(int empNumber)
        {
            try
            {
                var result = await _context.Employees
                    .Where(w => w.EmployeeNumber == empNumber)
                    .Select(s => new EmployeeDTO
                    {
                        FirstName = s.FirstName,
                        Lastname = s.Lastname,
                        EmployeeNumber = s.EmployeeNumber
                    }).FirstOrDefaultAsync();

                return new OperationResult<EmployeeDTO>
                {
                    Entity = result,
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeRepository.Update");

                return new OperationResult<EmployeeDTO>
                {
                    Message = "Error found.",
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
        }
    }
}
