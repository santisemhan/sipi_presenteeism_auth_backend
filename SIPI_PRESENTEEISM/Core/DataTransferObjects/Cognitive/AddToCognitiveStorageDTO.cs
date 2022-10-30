namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Cognitive
{
    using System.ComponentModel.DataAnnotations;

    public class AddToCognitiveStorageDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public List<IFormFile> Files { get; set; }
    }
}
