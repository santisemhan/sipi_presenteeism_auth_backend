namespace SIPI_PRESENTEEISM.Core.Repositories
{
    using SIPI_PRESENTEEISM.Core.Repositories.Interfaces;
    using SIPI_PRESENTEEISM.Data;

    public class StamentRepository : IStamentRepository
    {
        private readonly DataContext context;

        public StamentRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task<int> SaveChanges()
        {     
            return await context.SaveChangesAsync();       
        }
    }
}
