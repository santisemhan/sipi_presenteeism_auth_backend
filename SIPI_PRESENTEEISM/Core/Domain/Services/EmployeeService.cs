namespace SIPI_PRESENTEEISM.Core.Domain.Services
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using SIPI_PRESENTEEISM.Core.Domain.Entities;
    using SIPI_PRESENTEEISM.Core.Domain.Services.Interfaces;
    using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
    using System.Threading.Tasks;

    public class EmployeeService : IEmployeeService
    {
        private readonly IStamentRepository _stamentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IStamentRepository stamentRepository, IEmployeeRepository employeeRepository)
        {
            _stamentRepository = stamentRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<Guid> CreateEmployee(EmployeeCreateDTO info)
        {
            var employee = new Employee()
            {
                Name = info.Name,
                LastName = info.LastName,
                Email = info.LastName,
                Zone = new Zone()
                {
                    Name = info.Zone.Name,
                    RadioKm = info.Zone.RadioKm,
                    Latitude = info.Zone.Latitude,
                    Longitude = info.Zone.Longitude
                },
                ValidationCode = new Random().Next(11111, 99999),
                State = Enums.EmployeeState.To_Employee_Validation
            };

            await _employeeRepository.Add(employee);
            await _stamentRepository.SaveChanges();
            return employee.Id;
        }

        public async Task<EmployeeDTO> GetEmployee(int validationCode)
        {
            // BUG: Puede ser que multiples empleados tengan el mismo codigo de validacion
            var employee = await _employeeRepository
                .FindEmployee(e => e.ValidationCode == validationCode && e.State == Enums.EmployeeState.To_Employee_Validation);

            if (employee == null)
                throw new Exception("Employee not found");

            return new EmployeeDTO(employee);
        }

        public async Task<EmployeeDTO> GetEmployee(Guid userId)
        {
            var employee = await _employeeRepository
                .FindEmployee(e => e.Id == userId);

            if (employee == null)
                throw new Exception("Employee not found");

            return new EmployeeDTO(employee);
        }

        public async Task<List<EmployeeToListDTO>> GetAllEmployees()
        {
            var result = new List<EmployeeToListDTO>();

            var employees = await _employeeRepository.GetAllEmployees();
            employees.ForEach(e => result.Add(new EmployeeToListDTO(e)));

            return result;
        }
    }
}
