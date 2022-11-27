namespace SIPI_PRESENTEEISM.Core.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using SIPI_PRESENTEEISM.Core.Domain.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity([FromForm] ActivityDTO activity)
        {
            await _activityService.CreateActivityAsync(activity);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetActivities()
        {
            var result = await _activityService.GetActivitiesAsync();
            return Ok(result);
        }
    }
}
