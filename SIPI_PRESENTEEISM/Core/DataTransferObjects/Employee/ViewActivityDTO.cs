namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee
{
    using SIPI_PRESENTEEISM.Core.Domain.Entities;
    using SIPI_PRESENTEEISM.Core.Domain.Enums;

    public class ViewActivityDTO
    {
        public string EmployeeName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ActivityType ActivityType { get; set; }

        public string ImageURL { get; set; }

        public string Adress { get; set; }

        public DateTime TimeStamp { get; set; }

        public ViewActivityDTO()
        {

        }

        public ViewActivityDTO(Activity activity)
        {
            EmployeeName = activity.Employee.Name;
            Adress = activity.Adress;
            Latitude = activity.Latitude;
            Longitude = activity.Longitude;
            ActivityType = activity.ActivityType;
            ImageURL = activity.ImageURL;
            TimeStamp = activity.TimeStamp;
        }
    }
}
