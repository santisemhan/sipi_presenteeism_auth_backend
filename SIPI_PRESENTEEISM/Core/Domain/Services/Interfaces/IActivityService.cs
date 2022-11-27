namespace SIPI_PRESENTEEISM.Core.Domain.Services.Interfaces
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;

    public interface IActivityService
    {
        Task CreateActivityAsync(ActivityDTO activity);

        Task<List<ViewActivityDTO>> GetActivitiesAsync();
    }
}
