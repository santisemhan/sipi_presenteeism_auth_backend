namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Zone;
    using System.ComponentModel.DataAnnotations;

    public class EmployeeCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public ZoneDTO Zone { get; set; }
    }
}
