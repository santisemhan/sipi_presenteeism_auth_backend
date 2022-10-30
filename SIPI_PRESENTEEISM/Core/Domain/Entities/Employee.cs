namespace SIPI_PRESENTEEISM.Core.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public EmployeeState State { get; set; }

        public List<string> ImagesToIdentify { get; set; }
    }

    public enum EmployeeState
    {
        To_Employee_Validation,
        To_Admin_Validation,
        Validated,
        Blocked
    }
}
