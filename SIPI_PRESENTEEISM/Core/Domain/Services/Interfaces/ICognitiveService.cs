namespace SIPI_PRESENTEEISM.Core.Services.Interfaces
{
    using SIPI_PRESENTEEISM.Core.DataTransferObjects.Cognitive;

    public interface ICognitiveService
    {
        Task AddToCongniteStorage(Guid userId);

        Task<Guid?> IdentifyUser(IdentifyDTO info);
    }
}
