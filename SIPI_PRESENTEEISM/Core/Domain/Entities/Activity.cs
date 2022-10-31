namespace SIPI_PRESENTEEISM.Core.Domain.Entities
{
    using SIPI_PRESENTEEISM.Core.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Activity
    {
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ActivityType ActivityType { get; set; }

        public DateTime TimeStamp { get; }
    }
}
