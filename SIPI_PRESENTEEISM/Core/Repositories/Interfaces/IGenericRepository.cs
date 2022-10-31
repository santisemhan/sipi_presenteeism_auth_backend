namespace SIPI_PRESENTEEISM.Core.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
    }
}
