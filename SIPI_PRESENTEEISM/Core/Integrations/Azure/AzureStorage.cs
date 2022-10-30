namespace SIPI_PRESENTEEISM.Core.Integrations.Azure
{
    using global::Azure.Storage.Blobs;
    using global::Azure.Storage.Blobs.Models;
    using SIPI_PRESENTEEISM.Core.Integrations.Interfaces;

    public class AzureStorage : IStorage
    {
        private readonly string _container;

        public string ConnectionString { get; }
        public string BaseUrl { get; }

        public AzureStorage(IConfiguration configuration)
        {
            _container = configuration.GetValue<string>("AzureStorage:DefaultContainer");
            var accountName = configuration.GetValue<string>("AzureStorage:AccountName");
            var accessKey = configuration.GetValue<string>("AzureStorage:AccessKey");
            var endpointSuffix = configuration.GetValue<string>("AzureStorage:EndpointSuffix");
            var protocol = configuration.GetValue<string>("AzureStorage:Protocol");
            BaseUrl = configuration.GetValue<string>("AzureStorage:BaseUrl");
            ConnectionString = $@"DefaultEndpointsProtocol={protocol};AccountName={accountName};AccountKey={accessKey};EndpointSuffix={endpointSuffix}";
        }

        public async Task DeleteStream(Guid guid)
        {
            await DeleteStream(guid.ToString());
        }

        public async Task DeleteStream(string name)
        {
            var blobContainerClient = new BlobContainerClient(ConnectionString, _container);
            if (await blobContainerClient.ExistsAsync())
            {
                await blobContainerClient
                    .GetBlobClient(name)
                    .DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
            }
        }

        public async Task<Stream> DownloadStream(Guid guid)
        {
            return await DownloadStream(guid.ToString());
        }

        public async Task<Stream> DownloadStream(string name)
        {
            var blobContainerClient = new BlobContainerClient(ConnectionString, _container);
            if (await blobContainerClient.ExistsAsync())
            {
                return await blobContainerClient
                    .GetBlobClient(name)
                    .OpenReadAsync();
            }
            else return new MemoryStream();
        }

        public async Task<string> UploadStream(Stream stream, Guid guid)
        {
            return await UploadStream(stream, guid.ToString());
        }

        public async Task<string> UploadStream(Stream stream, string fileName)
        {
            var blobContainerClient = new BlobContainerClient(ConnectionString, _container);
            await blobContainerClient.CreateIfNotExistsAsync();
            await blobContainerClient.UploadBlobAsync(fileName, stream);
            return $"{BaseUrl}{_container}/{fileName}"; // Image Path 
        }
    }
}
