namespace LibrarySystem.Dto.Response;

public class PedidoResponseDto
{
    public int Id { get; set; }
    public DateTime FechaPedido { get; set; }

    public int ClienteId { get; set; }
    public string ClienteNombre { get; set; } = default!;

    public string EstadoPedido { get; set; } = default!;
    public ICollection<PedidoEjemplarResponseDto> Ejemplares { get; set; } = new List<PedidoEjemplarResponseDto>();
}