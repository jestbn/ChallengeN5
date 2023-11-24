using Domain.Abstractions;
using Domain.Permisos;
using Domain.TipoPermiso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence
{
    public sealed class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            try
            {
                var databasecreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databasecreator != null)
                {
                    if (!databasecreator.CanConnect())
                    {
                        databasecreator.Create();

                    }
                    //if (!databasecreator.HasTables()) databasecreator.CreateTables();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<TipoPermiso> TipoPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}