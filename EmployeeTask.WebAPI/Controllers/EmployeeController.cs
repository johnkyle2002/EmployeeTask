using EmployeeTask.Interface.Services;
using EmployeeTask.Shared.DataTrasferObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTask.WebAPI.Controllers
{
    [Authorize]
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


        [HttpGet("Get")]
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

        [HttpDelete]
        public async Task<ActionResult<IOperationResult>> Delete(int empNumber)
        {
            var result = await _employeeService.Remove(empNumber);

            return Ok(result);
        }
    }
}
