namespace SIPI_PRESENTEEISM.Core.Controllers
{
    using Mailjet.Client.Resources;
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

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> DownloadActivities([FromRoute] Guid userId)
        {
            var result = await _activityService.GetActivitiesByUser(userId);
            return Ok(result);
        }

        [HttpGet]
        [Route("download")]
        public async Task<IActionResult> DownloadActivities()
        {
            const string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            var result = await _activityService.DownloadActivityReport();

            return new FileStreamResult(result, contentType);
        }
    }
}
