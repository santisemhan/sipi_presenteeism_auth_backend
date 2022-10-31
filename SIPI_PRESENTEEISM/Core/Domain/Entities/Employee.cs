namespace SIPI_PRESENTEEISM.Core.Domain.Entities
{
    using SIPI_PRESENTEEISM.Core.Domain.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Employee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        [ForeignKey("Zone")]
        public int ZoneId { get; set; }
        public Zone Zone { get; set; }

        public int ValidationCode { get; set; }

        public EmployeeState State { get; set; }

        public List<string> ImagesToIdentify { get; set; }
    }
}
