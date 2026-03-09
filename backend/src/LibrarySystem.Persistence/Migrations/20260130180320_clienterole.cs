using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class clienterole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PedidoEjemplares",
                newName: "PedidoEjemplares",
                newSchema: "Biblioteca");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PedidoEjemplares",
                schema: "Biblioteca",
                newName: "PedidoEjemplares");
        }
    }
}
