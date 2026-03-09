using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class estadodisponible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Devuelto",
                schema: "Biblioteca",
                table: "PedidoEjemplares");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Devuelto",
                schema: "Biblioteca",
                table: "PedidoEjemplares",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
