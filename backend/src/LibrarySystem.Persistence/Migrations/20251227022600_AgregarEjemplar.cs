using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEjemplar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoLibros");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pedidos");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Libros");

            migrationBuilder.CreateTable(
                name: "Ejemplares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodigoBarra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    LibroId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejemplares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ejemplares_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PedidoEjemplares",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    EjemplarId = table.Column<int>(type: "int", nullable: false),
                    Devuelto = table.Column<bool>(type: "bit", nullable: false),
                    FechaDevolucion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoEjemplares", x => new { x.PedidoId, x.EjemplarId });
                    table.ForeignKey(
                        name: "FK_PedidoEjemplares_Ejemplares_EjemplarId",
                        column: x => x.EjemplarId,
                        principalTable: "Ejemplares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoEjemplares_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ejemplares_LibroId",
                table: "Ejemplares",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidoEjemplares_EjemplarId",
                table: "PedidoEjemplares",
                column: "EjemplarId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PedidoEjemplares");

            migrationBuilder.DropTable(
                name: "Ejemplares");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Pedidos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Libros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PedidoLibros",
                columns: table => new
                {
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    LibroId = table.Column<int>(type: "int", nullable: false),
                    Devuelto = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidoLibros", x => new { x.PedidoId, x.LibroId });
                    table.ForeignKey(
                        name: "FK_PedidoLibros_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidoLibros_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PedidoLibros_LibroId",
                table: "PedidoLibros",
                column: "LibroId");
        }
    }
}
