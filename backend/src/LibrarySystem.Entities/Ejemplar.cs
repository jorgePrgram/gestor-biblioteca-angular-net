using LibrarySystem.Entities.Enums;

namespace LibrarySystem.Entities;

public class Ejemplar: EntityBase
{
    public string CodigoBarra { get; set; } = default!;
    public EstadoEjemplar Estado { get; set; } = EstadoEjemplar.Disponible;
    public int LibroId { get; set; }
    public Libro Libro { get; set; } = default!;

    public ICollection<PedidoEjemplar> PedidoEjemplares { get; set; } = new List<PedidoEjemplar>();
}