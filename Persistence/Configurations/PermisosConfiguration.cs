using Domain.Permisos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class PermisosConfiguration : IEntityTypeConfiguration<Permiso>
    {
        public void Configure(EntityTypeBuilder<Permiso> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                permisoId => permisoId.value,
                value => new PermisoId(value))
                .ValueGeneratedOnAdd();

            /*builder.HasOne<TipoPermiso>(t => t.TipoPermiso)
                .WithMany()
                .HasForeignKey(p => p.TipoPermiso.Id);*/
        }
    }
}
