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
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IOperationResult<IList<EmployeeDTO>>>> GetAll()
        {
            var model = await _employeeService.GetAll();

            return Ok(model);
        }


        [HttpGet("Get/{empID}")]
        public async Task<ActionResult<IOperationResult<EmployeeDTO>>> Get(int empID)
        {
            if (empID == 0)
                return BadRequest("Invalid id.");

            var model = await _employeeService.Get(empID);

            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult<IOperationResult<int>>> Create(EmployeeDTO emp)
        {
            var result = await _employeeService.Create(emp);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<IOperationResult>> Update(EmployeeDTO emp)
        {
            var result = await _employeeService.Update(emp);
            return Ok(result);
        }

        [HttpDelete("{empNumber}")]
        public async Task<ActionResult<IOperationResult>> Delete(int empNumber)
        {
            var result = await _employeeService.Remove(empNumber);

            return Ok(result);
        }

        [HttpPost("GetByFilter")]
        public async Task<ActionResult<IOperationResult<IList<EmployeeDTO>>>> GetAllByFilter(IList<EmployeeFilterDTO> filters)
        {
            var result = await _employeeService.GetAllByFilter(filters);
            return Ok(result);
        }
    }
}
