namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee
{
    public class EmployeeToListDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public EmployeeToListDTO() { }

        public EmployeeToListDTO(Domain.Entities.Employee entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            LastName = entity.LastName;
            Email = entity.Email;
        }
    }
}
