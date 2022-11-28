namespace SIPI_PRESENTEEISM.Core.Domain.Entities
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee;
    using SIPI_PRESENTEEISM.Core.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Activity
    {
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ActivityType ActivityType { get; set; }

        public string ImageURL { get; set; }

        public string Adress { get; set; }

        public DateTime TimeStamp { get; }

        public ViewActivityDTO ToDto()
        {
            return new ViewActivityDTO(this);
        }
    }
}
