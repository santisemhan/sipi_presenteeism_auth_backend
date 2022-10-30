namespace SIPI_PRESENTEEISM.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Cognitive;
    using SIPI_PRESENTEEISM.Core.Services.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class CognitiveController : ControllerBase
    {
        private readonly ICognitiveService _cognitiveService;

        public CognitiveController(ICognitiveService cognitiveService)
        {
            _cognitiveService = cognitiveService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddToConginiteStorage([FromForm] AddToCognitiveStorageDTO info)
        {
            var images = new List<Stream>();
            info.Files.ForEach(file => images.Add(file.OpenReadStream()));

            await _cognitiveService.AddToCongniteStorage(info.UserId, images);
            return NoContent();
        }
    }
}
