using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Clientes_ClienteId",
                schema: "Biblioteca",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente",
                newSchema: "Biblioteca");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                schema: "Biblioteca",
                table: "Cliente",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Cliente_ClienteId",
                schema: "Biblioteca",
                table: "Pedido",
                column: "ClienteId",
                principalSchema: "Biblioteca",
                principalTable: "Cliente",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Cliente_ClienteId",
                schema: "Biblioteca",
                table: "Pedido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                schema: "Biblioteca",
                table: "Cliente");

            migrationBuilder.RenameTable(
                name: "Cliente",
                schema: "Biblioteca",
                newName: "Clientes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Clientes_ClienteId",
                schema: "Biblioteca",
                table: "Pedido",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
