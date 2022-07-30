using EmployeeTask.Interface.Services;
using EmployeeTask.Shared.DataTrasferObject;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTask.WebAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User, Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class TemperatureController : ControllerBase
    {
        private readonly IEmployeeTemperatureService _employeeTemperatureService;

        public TemperatureController(IEmployeeTemperatureService employeeTemperatureService)
        {
            _employeeTemperatureService = employeeTemperatureService;
        }

        [HttpPost]
        public async Task<ActionResult<IOperationResult<int?>>> Create(EmployeeTemperatureDTO temperature)
        {
            var result = await _employeeTemperatureService.Create(temperature);
            return Ok(result);
        }

        [HttpGet("Get/{tempID}")]
        public async Task<ActionResult<IOperationResult<EmployeeTemperatureDTO>>> Get(int tempID)
        {
            var result = await _employeeTemperatureService.Get(tempID);
            return Ok(result);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IOperationResult<IList<EmployeeTemperatureDTO>>>> GetAll()
        {
            var result = await _employeeTemperatureService.GetAll();
            return Ok(result);
        }

        [HttpDelete("{tempID}")]
        public async Task<ActionResult<IOperationResult>> Remove(int tempID)
        {
            var result = await _employeeTemperatureService.Remove(tempID);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<IOperationResult>> Update(EmployeeTemperatureDTO temperature)
        {
            var result = await _employeeTemperatureService.Update(temperature);
            return Ok(result);
        }
    }
}
