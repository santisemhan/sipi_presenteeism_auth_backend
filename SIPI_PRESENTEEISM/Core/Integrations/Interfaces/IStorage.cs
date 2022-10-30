namespace SIPI_PRESENTEEISM.Core.Integrations.Interfaces
{
    public interface IStorage
    {
        string ConnectionString { get; }
        string BaseUrl { get; }

        Task DeleteStream(Guid guid);
        Task DeleteStream(string name);

        Task<Stream> DownloadStream(Guid guid);
        Task<Stream> DownloadStream(string name);

        Task<string> UploadStream(Stream stream, Guid guid);
        Task<string> UploadStream(Stream stream, string name);
    }
}
