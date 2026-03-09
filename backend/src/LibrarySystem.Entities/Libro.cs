namespace LibrarySystem.Entities;

public class Libro: EntityBase
{
    public string Nombre { get; set; } = default!;
    public string Autor { get; set; } = default!;
    public string ISBN { get; set; } = default!;
    public string? ImageUrl { get; set; }


    public int GenreId { get; set; }
    public Genre Genre { get; set; } = default!;

    public ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();
}