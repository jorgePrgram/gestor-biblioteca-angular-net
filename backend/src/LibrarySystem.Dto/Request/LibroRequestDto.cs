using System.Text.Json.Serialization;

namespace LibrarySystem.Dto.Request;

public class LibroRequestDto
{
    public string Nombre { get; set; } = default!;
    public string Autor { get; set; } = default!;
    [JsonPropertyName("isbn")]
    public string ISBN { get; set; } = default!;
    public string? ImageUrl { get; set; }


    public int GenreId { get; set; }
}