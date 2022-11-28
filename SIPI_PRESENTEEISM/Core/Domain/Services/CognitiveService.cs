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
        private readonly IStamentRepository _stamentRepository;

        private readonly IFaceRecognition _faceRecognition;
        private readonly IStorage _storage;

        private readonly IEmployeeRepository _employeeRepository;

        public CognitiveService(IStamentRepository stamentRepository, IFaceRecognition faceRecognition, IStorage storage, IEmployeeRepository employeeRepository)
        {
            _stamentRepository = stamentRepository;
            _faceRecognition = faceRecognition;
            _storage = storage;
            _employeeRepository = employeeRepository;
        }

        public async Task AddToCongniteStorage(string userId, List<IFormFile> images)
        {
            var employee = await _employeeRepository
                .FindEmployee(e => e.Id == Guid.Parse(userId) && e.State == EmployeeState.To_Employee_Validation);

            if (employee == null)
                throw new Exception("Employee not found");

            employee.State = EmployeeState.To_Admin_Validation;

            var imagesURL = new List<string>();
            foreach (var image in images)
            {
                IList<string> allowedFileExtensions = new List<string> { ".jpeg", ".jpg", ".png" };
                var extension = image.FileName.Substring(image.FileName.LastIndexOf('.')).ToLower();

                if (!allowedFileExtensions.Contains(extension.ToString()))
                    throw new Exception("Format not valid");

                var imageURL = await _storage.UploadStream(image.OpenReadStream(), $"{Guid.NewGuid()}{extension}");
                employee.ImagesToIdentify.Add(new ImageToIdentify()
                {
                    Employee = employee,
                    ImageURL = imageURL
                });

                imagesURL.Add(imageURL);
            }

            await _faceRecognition.AddPerson(Guid.Parse(userId), imagesURL);

            await _stamentRepository.SaveChanges();
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
