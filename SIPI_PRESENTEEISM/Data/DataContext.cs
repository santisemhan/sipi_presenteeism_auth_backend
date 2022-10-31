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
        public DbSet<Zone> Zone { get; set; }
        public DbSet<Activity> Activity { get; set; }


        /// <summary>
        ///     Se utiliza para la construccion de los modelos con Entity Framework.
        ///     Esta estrategia es mas conocida como fluent api, y la utilizo solamente cuando
        ///     no existen anotaciones para funciones especificas que se pueden logran con dicha 
        ///     estrategia.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            // Fecha y Horario actual en Argentina.
            var sqlToday = "CAST(SWITCHOFFSET(SYSDATETIMEOFFSET(), '-03:00') AS DATETIME2(3))";

            // Al crear una entity autogenera un valor de TimeStamp
            // para saber en el momento exacto en el cual se creo
            modelBuilder.Entity<Activity>()
                .Property(b => b.TimeStamp)
                .HasDefaultValueSql(sqlToday)
                .ValueGeneratedOnAdd();

            // Clustered Key
            modelBuilder.Entity<Activity>()
                .HasKey(b => new { b.EmployeeId, b.TimeStamp });

            modelBuilder.Entity<ImageToIdentify>()
                .HasKey(b => new { b.EmployeeId, b.ImageURL });
        }
    }
}
