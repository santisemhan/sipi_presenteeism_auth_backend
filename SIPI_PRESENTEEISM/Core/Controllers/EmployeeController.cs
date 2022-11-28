namespace SIPI_PRESENTEEISM.Core.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Zone;
    using SIPI_PRESENTEEISM.Core.Domain.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDTO info)
        {
            var result = await _employeeService.CreateEmployee(info);
            return Ok(result);
        }

        [HttpGet]
        [Route("employee-validation")]
        public async Task<IActionResult> GetEmployee([FromQuery] int validationCode)
        {
            var result = await _employeeService.GetEmployee(validationCode);
            return Ok(result);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetEmployee([FromRoute] string userId)
        {
            var result = await _employeeService.GetEmployee(Guid.Parse(userId));
            return Ok(result);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetEmployees()
        {
            var result = await _employeeService.GetAllEmployees();
            return Ok(result);
        }

        [HttpPost]
        [Route("validate/{userId}/{byEmployee}")]
        public async Task<IActionResult> ValidateEmployee([FromRoute]Guid userId,[FromRoute] bool byEmployee)
        {
            await _employeeService.ValidateEmployee(userId, byEmployee);
            return NoContent();
        }

        [HttpPost]
        [Route("validate-zone/{employeeId}")]
        public async Task<IActionResult> ValidateZone([FromRoute]Guid employeeId,[FromBody] ValidateLocationDTO zone)
        {
            await _employeeService.ValidateZone(employeeId, zone);
            return NoContent();
        }

        [HttpPost]
        [Route("registration/upload")]
        public async Task<IActionResult> UploadRegistrationByEmployee([FromForm] UploadRegistrationDTO info)
        {
            await _employeeService.UploadRegistrationByEmployee(info);
            return NoContent();
        }
    }
}
