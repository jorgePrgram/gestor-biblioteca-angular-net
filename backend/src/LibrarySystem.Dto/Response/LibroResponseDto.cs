namespace LibrarySystem.Dto.Response;

public class LibroResponseDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = default!;
    public string Autor { get; set; } = default!;
    public string ISBN { get; set; } = default!;
    public string? ImageUrl { get; set; }

    public int GenreId { get; set; }
    public string GenreNombre { get; set; } = default!;

    // Propiedad calculada
    public string EstadoDescripcion { get; set; } = default!;
    public int StockDisponible { get; set; }
}