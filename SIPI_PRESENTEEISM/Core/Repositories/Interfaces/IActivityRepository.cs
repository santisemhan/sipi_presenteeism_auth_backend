namespace SIPI_PRESENTEEISM.Core.Repositories.Interfaces
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using System;

    public interface IActivityRepository
    {
        Task CreateActivityAsync(ActivityDTO activity);

        Task<List<ViewActivityDTO>> GetActivitiesAsync();

        Task<List<ViewActivityDTO>> GetActivitiesByUser(Guid userId);
    }
}
