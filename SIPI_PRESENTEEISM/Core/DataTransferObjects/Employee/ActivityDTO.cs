namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee
{
    using SIPI_PRESENTEEISM.Core.Domain.Enums;

    public class ActivityDTO
    {
        public Guid EmployeeId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public IFormFile Image { get; set; }
    }
}
