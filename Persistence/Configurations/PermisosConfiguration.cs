using Domain.Permisos;
using Domain.TipoPermiso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class PermisosConfiguration : IEntityTypeConfiguration<Permisos>
    {
        public void Configure(EntityTypeBuilder<Permisos> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Id).HasConversion(
                permisoId => permisoId.value,
                value => new PermisoId(value))
                .ValueGeneratedOnAdd();

            builder.HasOne<TipoPermisos>(t => t.TipoPermiso)
                .WithMany()
                .HasForeignKey(p => p.TipoPermiso);
        }
    }
}
