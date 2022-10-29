namespace SIPI_PRESENTEEISM.Core.Integrations.Interfaces
{
    public interface IAzureFaceRecognition
    {
        Task AddPerson(Guid userId, List<Stream> images);

        Task<bool> Identify(Guid userId, Stream image);
    }
}
