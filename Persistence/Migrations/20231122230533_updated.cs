using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TipoPermisos",
                columns: new[] { "Descripcion" },
                values: new object[] { "Vacaciones" });

            migrationBuilder.InsertData(
                table: "TipoPermisos",
                columns: new[] { "Descripcion" },
                values: new object[] { "Medico" });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE TipoPermisos; DBCC CHECKIDENT('TipoPermisos', RESEED, 0);");
        }
    }
}
