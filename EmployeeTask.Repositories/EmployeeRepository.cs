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
                        StatusCode = StatusCodeEnum.Code.BadRequest,
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
                    Entity = model.EmployeeNumber,
                    StatusCode = StatusCodeEnum.Code.Ok,
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

                return new OperationResult
                {
                    StatusCode = StatusCodeEnum.Code.Ok,
                    Message = "Successfully saved!"
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
                _logger.LogError(ex, $"EmployeeRepository.Remove");

                return new OperationResult
                {
                    StatusCode = StatusCodeEnum.Code.InternalError,
                    Message = "Error found."
                };
            }
        } 
    }
}
