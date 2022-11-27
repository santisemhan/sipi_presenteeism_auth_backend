namespace SIPI_PRESENTEEISM.Core.Repositories.Interfaces
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;

    public interface IActivityRepository
    {
        Task CreateActivityAsync(ActivityDTO activity);

        Task<List<ViewActivityDTO>> GetActivitiesAsync();
    }
}
