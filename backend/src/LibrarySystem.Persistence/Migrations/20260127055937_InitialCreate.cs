using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ejemplares_Libros_LibroId",
                table: "Ejemplares");

            migrationBuilder.DropForeignKey(
                name: "FK_Libros_Genre_GenreId",
                table: "Libros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libros",
                table: "Libros");

            migrationBuilder.RenameTable(
                name: "Libros",
                newName: "Libro",
                newSchema: "Biblioteca");

            migrationBuilder.RenameIndex(
                name: "IX_Libros_GenreId",
                schema: "Biblioteca",
                table: "Libro",
                newName: "IX_Libro_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libro",
                schema: "Biblioteca",
                table: "Libro",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ejemplares_Libro_LibroId",
                table: "Ejemplares",
                column: "LibroId",
                principalSchema: "Biblioteca",
                principalTable: "Libro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Libro_Genre_GenreId",
                schema: "Biblioteca",
                table: "Libro",
                column: "GenreId",
                principalSchema: "Biblioteca",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ejemplares_Libro_LibroId",
                table: "Ejemplares");

            migrationBuilder.DropForeignKey(
                name: "FK_Libro_Genre_GenreId",
                schema: "Biblioteca",
                table: "Libro");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Libro",
                schema: "Biblioteca",
                table: "Libro");

            migrationBuilder.RenameTable(
                name: "Libro",
                schema: "Biblioteca",
                newName: "Libros");

            migrationBuilder.RenameIndex(
                name: "IX_Libro_GenreId",
                table: "Libros",
                newName: "IX_Libros_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libros",
                table: "Libros",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ejemplares_Libros_LibroId",
                table: "Ejemplares",
                column: "LibroId",
                principalTable: "Libros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Libros_Genre_GenreId",
                table: "Libros",
                column: "GenreId",
                principalSchema: "Biblioteca",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
