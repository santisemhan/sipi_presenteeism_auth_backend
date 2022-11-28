namespace SIPI_PRESENTEEISM.Core.DataTransferObjects.Employee
{
    using System.ComponentModel.DataAnnotations;

    public class UploadRegistrationDTO
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public List<IFormFile> Files { get; set; }
    }
}
