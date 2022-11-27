namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee
{
    using SIPI_PRESENTEEISM.Core.Domain.Entities;
    using SIPI_PRESENTEEISM.Core.Domain.Enums;

    public class ViewActivityDTO
    {
        public Guid EmployeeId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ActivityType ActivityType { get; set; }

        public string ImageURL { get; set; }

        public DateTime TimeStamp { get; set; }

        public ViewActivityDTO()
        {

        }

        public ViewActivityDTO(Activity activity)
        {
            EmployeeId = activity.EmployeeId;
            Latitude = activity.Latitude;
            Longitude = activity.Longitude;
            ActivityType = activity.ActivityType;
            ImageURL = activity.ImageURL;
            TimeStamp = activity.TimeStamp;
        }
    }
}
