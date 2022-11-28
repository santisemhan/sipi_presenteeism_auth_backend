namespace SIPI_PRESENTEEISM.Core.Domain.Services
{
    using Newtonsoft.Json.Linq;
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using SIPI_PRESENTEEISM.Core.Domain.Entities;
    using SIPI_PRESENTEEISM.Core.Domain.Enums;
    using SIPI_PRESENTEEISM.Core.Domain.Services.Interfaces;
    using SIPI_PRESENTEEISM.Core.Integrations.Interfaces;
    using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
    using System.Threading.Tasks;
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Zone;

    public class EmployeeService : IEmployeeService
    {
        private readonly IStamentRepository _stamentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        private readonly IMail _mailing;

        public EmployeeService(IStamentRepository stamentRepository, IEmployeeRepository employeeRepository, IMail mailing)
        {
            _stamentRepository = stamentRepository;
            _employeeRepository = employeeRepository;
            _mailing = mailing;
        }

        public async Task<Guid> CreateEmployee(EmployeeCreateDTO info)
        {
            var employee = new Employee()
            {
                Name = info.Name,
                LastName = info.LastName,
                Email = info.Email,
                Zone = new Zone()
                {
                    Name = info.Zone.Name,
                    RadioKm = info.Zone.RadioKm,
                    Latitude = info.Zone.Latitude,
                    Longitude = info.Zone.Longitude
                },
                ValidationCode = new Random().Next(11111, 99999),
                State = EmployeeState.To_Employee_Validation
            };

            await _employeeRepository.Add(employee);
            await _stamentRepository.SaveChanges();

            await _mailing.SendEmail(new JArray { new JObject { {"Email", info.Email } } }, "CODIGO DE VALIDACION DE CUENTA - SIPI UADE",
                 "Ingresá el código en la aplicacion para completar el proceso de registro de tu cuenta",
                 $"<h3>Tu codigo de validacion es {employee.ValidationCode}</h3>");

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

        public async Task ValidateEmployee(Guid userId, bool byEmployee)
        {
            var employee = await _employeeRepository.FindEmployee(e => e.Id == userId);

            if (employee == null)
                throw new Exception("Employee not found");

            employee.State = byEmployee ? EmployeeState.To_Admin_Validation : EmployeeState.Validated;
        }

        public async Task<bool> ValidateZone(Guid employeId, ZoneDTO zone)
        {
            var TO_RADIAN = 57.29577951; // 180 / π (pi)
            var TO_KM = 6378.8;

            var employee = await _employeeRepository.FindEmployee(e => e.Id == employeId);

            if (employee == null)
                throw new Exception("Employee not found");

            var latToValidate = zone.Latitude / TO_RADIAN;
            var lngToValidate = zone.Longitude / TO_RADIAN;
            var latZone = employee.Zone.Latitude / TO_RADIAN;
            var lngZone = employee.Zone.Longitude / TO_RADIAN;

            var distance = TO_KM * Math.Acos(Math.Sin(latToValidate) * Math.Sin(latZone) + Math.Cos(latToValidate) * Math.Cos(latZone) * Math.Cos(lngZone - lngToValidate));

            if (distance > employee.Zone.RadioKm)
                throw new Exception("El empleado no se encuentra en la zona de trabajo");

            return true;
        }
    }
}
