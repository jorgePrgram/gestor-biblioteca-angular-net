namespace LibrarySystem.Entities;

public class PedidoEjemplar
{
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = default!;

    public int EjemplarId { get; set; }
    public Ejemplar Ejemplar { get; set; } = default!;

    public DateTime FechaReserva { get; set; } = DateTime.Now;


    public DateTime? FechaDevolucion { get; set; }
    public DateTime? FechaPrestamo { get; set; }

}