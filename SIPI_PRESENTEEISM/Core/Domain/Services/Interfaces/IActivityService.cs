namespace SIPI_PRESENTEEISM.Core.Domain.Services.Interfaces
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using System;

    public interface IActivityService
    {
        Task CreateActivityAsync(ActivityDTO activity);

        Task<List<ViewActivityDTO>> GetActivitiesAsync();

        Task<List<ViewActivityDTO>> GetActivitiesByUser(Guid userId);

        Task<MemoryStream> DownloadActivityReport();
    }
}
