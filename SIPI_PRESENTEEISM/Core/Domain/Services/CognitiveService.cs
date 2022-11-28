namespace SIPI_PRESENTEEISM.Core.Services
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Cognitive;
    using SIPI_PRESENTEEISM.Core.Domain.Entities;
    using SIPI_PRESENTEEISM.Core.Domain.Enums;
    using SIPI_PRESENTEEISM.Core.Integrations.Interfaces;
    using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
    using SIPI_PRESENTEEISM.Core.Services.Interfaces;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CognitiveService : ICognitiveService
    {
        private readonly IFaceRecognition _faceRecognition;
        private readonly IStorage _storage;

        private readonly IEmployeeRepository _employeeRepository;

        public CognitiveService(IFaceRecognition faceRecognition, IStorage storage, IEmployeeRepository employeeRepository)
        {
            _faceRecognition = faceRecognition;
            _storage = storage;
            _employeeRepository = employeeRepository;
        }

        public async Task AddToCongniteStorage(Guid userId)
        {
            var employee = await _employeeRepository
                .FindEmployee(e => e.Id == userId && e.State == EmployeeState.Validated);

            if (employee == null)
                throw new Exception("Employee not found");

            await _faceRecognition.AddPerson(userId, employee.ImagesToIdentify.Select(x => x.ImageURL).ToList());
        }

        public async Task<Guid?> IdentifyUser(IdentifyDTO info)
        {
            var image = info.File;

            IList<string> allowedFileExtensions = new List<string> { ".jpeg", ".jpg", ".png" };
            var extension = image.FileName.Substring(image.FileName.LastIndexOf('.')).ToLower();

            if (!allowedFileExtensions.Contains(extension.ToString()))
                throw new Exception("Format not valid");

            var imageURL = await _storage.UploadStream(image.OpenReadStream(), $"{Guid.NewGuid()}{extension}");
            return await _faceRecognition.Identify(imageURL);
        }
    }
}
