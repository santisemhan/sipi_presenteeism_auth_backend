namespace SIPI_PRESENTEEISM.Core.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using SIPI_PRESENTEEISM.Core.Domain.Enums;
    using SIPI_PRESENTEEISM.Core.Integrations.Interfaces;
    using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
    using SIPI_PRESENTEEISM.Data;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ActivityRepository : IActivityRepository
    {
        private readonly DataContext _dataContext;
        private readonly IStamentRepository _stamentRepository;
        private readonly IStorage _storage;

        public ActivityRepository(DataContext dataContext, IStamentRepository stamentRepository, IStorage storage)
        {
            _dataContext = dataContext;
            _stamentRepository = stamentRepository;
            _storage = storage;
        }

        public async Task CreateActivityAsync(ActivityDTO activity)
        {
            var url = await _storage.UploadStream(activity.Image.OpenReadStream(), Guid.NewGuid());
            await _dataContext.Activity.AddAsync(new Domain.Entities.Activity()
            {
                EmployeeId = activity.EmployeeId,
                Latitude = activity.Latitude,
                Longitude = activity.Longitude,
                ActivityType = ActivityType.CheckIn, // Por ahora no diferenciamos si es Out/In
                ImageURL = url
            });
            await _stamentRepository.SaveChanges();
        }

        public async Task<List<ViewActivityDTO>> GetActivitiesAsync()
        {
            return (await _dataContext.Activity
                .ToListAsync())
                .Select(a => a.ToDto())
                .ToList();
        }
    }
}
