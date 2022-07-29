using EmployeeTask.Interface.Repositories;
using EmployeeTask.Models;
using EmployeeTask.Shared.DataTrasferObject;
using EmployeeTask.Shared.Enumerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeTask.Repositories
{
    public class EmployeeTemperatureRepository: IEmployeeTemperatureRepository
    {
        private readonly EmployeeTaskDBContext _context;
        private readonly ILogger<EmployeeTemperatureRepository> _logger;

        public EmployeeTemperatureRepository(EmployeeTaskDBContext context,
            ILogger<EmployeeTemperatureRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IOperationResult<int?>> Create(EmployeeTemperatureDTO temperature)
        {
            try
            {
                if (temperature is null)
                    return new OperationResult<int?>
                    {
                        StatusCode = StatusCodeEnum.Code.BadRequest,
                        Message = "Employee must not be null."
                    };

                var model = new EmployeeTemperature
                {
                    EmployeeNumber = temperature.EmployeeNumber,
                    Temperature = temperature.Temperature
                };

                await _context.Temparatures.AddAsync(model);
                await _context.SaveChangesAsync();

                return new OperationResult<int?>
                {
                    StatusCode = StatusCodeEnum.Code.Ok,
                    Message = "Successfully created."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeTemperatureRepository.Create");

                return new OperationResult<int?>
                {
                    StatusCode = StatusCodeEnum.Code.InternalError,
                    Message = "Error found."
                };
            }
        }

        public async Task<IOperationResult> Update(EmployeeTemperatureDTO temperature)
        {
            try
            {
                var model = _context.Temparatures.FirstOrDefault(e => e.EmployeeTemperatureID == temperature.EmployeeTemperatureID);

                if (model is null)
                    return new OperationResult<EmployeeTemperatureDTO>
                    {
                        Message = "Not found",
                        StatusCode = StatusCodeEnum.Code.BadRequest
                    };

                model.RecordDate = temperature.RecordDate;
                model.Temperature = temperature.Temperature;

                _context.Temparatures.Update(model);
                await _context.SaveChangesAsync();

                return new OperationResult<EmployeeTemperatureDTO>
                {
                    StatusCode = StatusCodeEnum.Code.Ok,
                    Message = "Successfully saved!"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeTemperatureRepository.Update");

                return new OperationResult<EmployeeTemperatureDTO>
                {
                    StatusCode = StatusCodeEnum.Code.InternalError,
                    Message = "Error found."
                };
            }
        }

        public async Task<IOperationResult> Remove(int tempID)
        {
            try
            {
                var model = _context.Temparatures.FirstOrDefault(e => e.EmployeeTemperatureID == tempID);

                if (model is null)
                    return new OperationResult
                    {
                        StatusCode = StatusCodeEnum.Code.BadRequest,
                        Message = "Not found."
                    };

                _context.Temparatures.Remove(model);
                await _context.SaveChangesAsync();

                return new OperationResult
                {
                    StatusCode = StatusCodeEnum.Code.Ok,
                    Message = "Successfully removed."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeTemperatureRepository.Remove");

                return new OperationResult
                {
                    StatusCode = StatusCodeEnum.Code.InternalError,
                    Message = "Error found."
                };
            }
        } 
    }
}
