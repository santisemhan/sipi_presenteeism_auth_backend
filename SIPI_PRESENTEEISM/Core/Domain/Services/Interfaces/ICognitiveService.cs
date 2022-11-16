namespace SIPI_PRESENTEEISM.Core.Services.Interfaces
{
    public interface ICognitiveService
    {
        Task AddToCongniteStorage(string userId, List<IFormFile> images);

        Task<bool> IdentifyUser(string userId, Stream image);
    }
}
