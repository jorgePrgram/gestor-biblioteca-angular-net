namespace LibrarySystem.Entities;

public class Pedido: EntityBase
{
    public DateTime FechaPedido { get; set; }=DateTime.Now;

    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } = default!;

    public ICollection<PedidoEjemplar> PedidoEjemplares { get; set; } = new List<PedidoEjemplar>();
}
