using Domain.TipoPermiso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class TipoPermisosConfiguration : IEntityTypeConfiguration<TipoPermisos>
    {
        public void Configure(EntityTypeBuilder<TipoPermisos> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x=>x.Id).HasConversion(
                tipoPermisoId => tipoPermisoId.value,
                value => new TipoPermisoId(value))
                .ValueGeneratedOnAdd();
        }
    }
}
