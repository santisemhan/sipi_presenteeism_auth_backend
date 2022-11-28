namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee
{
    public class ActivityDTO
    {
        public Guid EmployeeId { get; set; }

        public string Adress { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public IFormFile Image { get; set; }
    }
}
