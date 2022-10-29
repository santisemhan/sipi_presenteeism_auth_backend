namespace SIPI_PRESENTEEISM.Core.Services
{
    using SIPI_PRESENTEEISM.Core.Domain.Entities;
    using SIPI_PRESENTEEISM.Core.Integrations.Interfaces;
    using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
    using SIPI_PRESENTEEISM.Core.Services.Interfaces;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class CognitiveService : ICognitiveService
    {
        private readonly IStamentRepository _stamentRepository;

        private readonly IAzureFaceRecognition _azureFaceRecognition;

        private readonly IEmployeeRepository _employeeRepository;

        public CognitiveService(IStamentRepository stamentRepository, IAzureFaceRecognition azureFaceRecognition, IEmployeeRepository employeeRepository)
        {
            _stamentRepository = stamentRepository; 
            _azureFaceRecognition = azureFaceRecognition;
            _employeeRepository = employeeRepository;
        }

        public async Task AddToCongniteStorage(string userId, List<Stream> images)
        {
            var employee = await _employeeRepository
                .FindEmployee(e => e.Id == Guid.Parse(userId) && e.State == EmployeeState.To_Employee_Validation);

            if (employee == null)
                throw new Exception("Employee not found");

            await _azureFaceRecognition.AddPerson(Guid.Parse(userId), images);

            employee.State = EmployeeState.To_Admin_Validation;
            await _stamentRepository.SaveChanges();
        }

        public async Task<bool> IdentifyUser(string userId, Stream image)
        {
            return await _azureFaceRecognition.Identify(Guid.Parse(userId), image);
        }
    }
}
