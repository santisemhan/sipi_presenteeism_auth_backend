namespace SIPI_PRESENTEEISM.Core.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public class ImageToIdentify
    {
        [ForeignKey("Employee")]
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public string ImageURL { get; set; }
    }
}
