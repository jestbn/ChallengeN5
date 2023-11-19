using Domain.TipoPermiso;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    internal class TipoPermisosConfiguration : IEntityTypeConfiguration<TipoPermiso>
    {
        public void Configure(EntityTypeBuilder<TipoPermiso> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                tipoPermisoId => tipoPermisoId.value,
                value => new TipoPermisoId(value))
                .ValueGeneratedOnAdd();
        }
    }
}
