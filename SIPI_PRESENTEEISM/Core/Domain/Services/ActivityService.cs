namespace SIPI_PRESENTEEISM.Core.Domain.Services
{
    using OfficeOpenXml;
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using SIPI_PRESENTEEISM.Core.Domain.Services.Interfaces;
    using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
    using System;
    using System.IO;
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

        public async Task<MemoryStream> DownloadActivityReport()
        {
            var memoryStream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(memoryStream))
            {
                var worksheet = package.Workbook.Worksheets.Add("MaterialsPiqueo");

                worksheet.Cells[1, 1].Value = "Empleado";
                worksheet.Cells[1, 2].Value = "Fecha";
                worksheet.Cells[1, 3].Value = "Latitud";
                worksheet.Cells[1, 4].Value = "Longitud";
                worksheet.Cells[1, 5].Value = "Dirección";
                worksheet.Cells[1, 6].Value = "Imagen tomada";

                var activities = await _activityRepository.GetActivitiesAsync();
                for (var index = 0; index < activities.Count; index++)
                {
                    var activity = activities[index];
                    var row = index + 2;

                    worksheet.Cells[row, 1].Value = activity.EmployeeName;
                    worksheet.Cells[row, 2].Value = activity.TimeStamp;
                    worksheet.Cells[row, 3].Value = activity.Latitude;
                    worksheet.Cells[row, 4].Value = activity.Longitude;
                    worksheet.Cells[row, 5].Value = activity.Adress;
                    worksheet.Cells[row, 6].Value = activity.ImageURL;
                }

                package.Save();
            }

            memoryStream.Position = 0;
            return memoryStream;
        }

        public async Task<List<ViewActivityDTO>> GetActivitiesAsync()
        {
            return await _activityRepository.GetActivitiesAsync();
        }

        public async Task<List<ViewActivityDTO>> GetActivitiesByUser(Guid userId)
        {
            return await _activityRepository.GetActivitiesByUser(userId);
        }
    }
}
