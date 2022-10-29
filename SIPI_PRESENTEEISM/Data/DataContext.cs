namespace SIPI_PRESENTEEISM.Data
{
    using Microsoft.EntityFrameworkCore;
    using SIPI_PRESENTEEISM.Core.Domain.Entities;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
