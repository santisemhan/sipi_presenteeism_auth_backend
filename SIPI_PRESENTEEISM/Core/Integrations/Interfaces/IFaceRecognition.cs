namespace SIPI_PRESENTEEISM.Core.Integrations.Interfaces
{
    public interface IFaceRecognition
    {
        Task AddPerson(Guid userId, List<Stream> images);

        Task AddPerson(Guid userId, List<string> imagesURL);

        Task<Guid?> Identify(string imageURL);
    }
}
