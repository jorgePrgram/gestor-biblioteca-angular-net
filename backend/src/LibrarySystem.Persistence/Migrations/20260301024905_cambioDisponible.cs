using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class cambioDisponible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disponible",
                schema: "Biblioteca",
                table: "Ejemplares");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaPrestamo",
                schema: "Biblioteca",
                table: "PedidoEjemplares",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                schema: "Biblioteca",
                table: "Ejemplares",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaPrestamo",
                schema: "Biblioteca",
                table: "PedidoEjemplares");

            migrationBuilder.DropColumn(
                name: "Estado",
                schema: "Biblioteca",
                table: "Ejemplares");

            migrationBuilder.AddColumn<bool>(
                name: "Disponible",
                schema: "Biblioteca",
                table: "Ejemplares",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
