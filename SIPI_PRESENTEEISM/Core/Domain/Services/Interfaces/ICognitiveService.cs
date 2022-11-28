namespace SIPI_PRESENTEEISM.Core.Services.Interfaces
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Cognitive;

    public interface ICognitiveService
    {
        Task AddToCongniteStorage(string userId, List<IFormFile> images);

        Task<Guid?> IdentifyUser(IdentifyDTO info);
    }
}
