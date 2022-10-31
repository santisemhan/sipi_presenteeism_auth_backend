namespace SIPI_PRESENTEEISM.Core.Domain.Services.Interfaces
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;

    public interface IEmployeeService
    {
        Task<Guid> CreateEmployee(EmployeeCreateDTO info);

        Task<EmployeeDTO> GetEmployee(int validationCode);
        Task<EmployeeDTO> GetEmployee(Guid userId);

        Task<List<EmployeeToListDTO>> GetAllEmployees();

        Task ValidateEmployee(Guid userId, bool byEmployee);
    }
}
