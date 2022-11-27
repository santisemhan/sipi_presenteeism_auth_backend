namespace SIPI_PRESENTEEISM.Core.Domain.Services
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using SIPI_PRESENTEEISM.Core.Domain.Services.Interfaces;
    using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
    using System.Threading.Tasks;

    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task CreateActivityAsync(ActivityDTO activity)
        {
            await _activityRepository.CreateActivityAsync(activity);
        }

        public async Task<List<ViewActivityDTO>> GetActivitiesAsync()
        {
            return await _activityRepository.GetActivitiesAsync();
        }
    }
}
