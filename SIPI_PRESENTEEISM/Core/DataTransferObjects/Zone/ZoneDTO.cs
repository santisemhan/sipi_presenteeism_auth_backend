namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Zone
{
    public class ZoneDTO
    {
        public string Name { get; set; }

        public double RadioKm { get; set; } = 0.5;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
