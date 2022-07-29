using EmployeeTask.Interface.Repositories;
using EmployeeTask.Interface.Services;
using EmployeeTask.Shared.DataTrasferObject;
using EmployeeTask.Shared.Enumerator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EmployeeTask.Services
{
    public class EmployeeTemperatureService : IEmployeeTemperatureService
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly IEmployeeTemperatureRepository _repository;
        private readonly IQueryableRepository _query;

        public EmployeeTemperatureService(ILogger<EmployeeService> logger,
            IEmployeeTemperatureRepository repository,
            IQueryableRepository query)
        {
            _logger = logger;
            _repository = repository;
            _query = query;
        }

        public async Task<IOperationResult<int?>> Create(EmployeeTemperatureDTO temperature)
        {
            return await _repository.Create(temperature);
        }

        public async Task<IOperationResult<EmployeeTemperatureDTO>> Get(int tempID)
        {
            try
            {
                var result = await _query.EmpoyeeTemperatureQUery()
                    .Where(w => w.EmployeeTemperatureID == tempID)
                    .FirstOrDefaultAsync();

                if (result is null)
                    return new OperationResult<EmployeeTemperatureDTO>
                    {
                        StatusCode = StatusCodeEnum.Code.BadRequest,
                        Message = "Not found."
                    };

                return new OperationResult<EmployeeTemperatureDTO>
                {
                    Entity = result,
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeTemperatureService.Get");

                return new OperationResult<EmployeeTemperatureDTO>
                {
                    Message = "Error found.",
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
        }

        public async Task<IOperationResult<IList<EmployeeTemperatureDTO>>> GetAll()
        {
            try
            {
                var result = await _query.EmpoyeeTemperatureQUery().ToListAsync();

                return new OperationResult<IList<EmployeeTemperatureDTO>>
                {
                    Entity = result,
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmployeeTemperatureService.GetAll");

                return new OperationResult<IList<EmployeeTemperatureDTO>>
                {
                    Message = "Error found.",
                    StatusCode = StatusCodeEnum.Code.Ok,
                };
            }
        }

        public async Task<IOperationResult> Remove(int tempID)
        {
            return await _repository.Remove(tempID);
        }

        public async Task<IOperationResult> Update(EmployeeTemperatureDTO temperature)
        {
            return await _repository.Update(temperature);
        }
    }
}
