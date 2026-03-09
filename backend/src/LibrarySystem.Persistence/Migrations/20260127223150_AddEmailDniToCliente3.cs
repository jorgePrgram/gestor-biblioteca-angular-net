using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailDniToCliente3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoEjemplares_Pedidos_PedidoId",
                table: "PedidoEjemplares");

            migrationBuilder.DropForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos");

            migrationBuilder.RenameTable(
                name: "Pedidos",
                newName: "Pedido",
                newSchema: "Biblioteca");

            migrationBuilder.RenameIndex(
                name: "IX_Pedidos_ClienteId",
                schema: "Biblioteca",
                table: "Pedido",
                newName: "IX_Pedido_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedido",
                schema: "Biblioteca",
                table: "Pedido",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pedido_Clientes_ClienteId",
                schema: "Biblioteca",
                table: "Pedido",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoEjemplares_Pedido_PedidoId",
                table: "PedidoEjemplares",
                column: "PedidoId",
                principalSchema: "Biblioteca",
                principalTable: "Pedido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pedido_Clientes_ClienteId",
                schema: "Biblioteca",
                table: "Pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoEjemplares_Pedido_PedidoId",
                table: "PedidoEjemplares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pedido",
                schema: "Biblioteca",
                table: "Pedido");

            migrationBuilder.RenameTable(
                name: "Pedido",
                schema: "Biblioteca",
                newName: "Pedidos");

            migrationBuilder.RenameIndex(
                name: "IX_Pedido_ClienteId",
                table: "Pedidos",
                newName: "IX_Pedidos_ClienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pedidos",
                table: "Pedidos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoEjemplares_Pedidos_PedidoId",
                table: "PedidoEjemplares",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pedidos_Clientes_ClienteId",
                table: "Pedidos",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
