    namespace SIPI_PRESENTEEISM.Core.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Zone
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double RadioKm { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
