namespace LibrarySystem.Dto.Response;

public class PedidoEjemplarResponseDto

{
    public int EjemplarId { get; set; }
    public string CodigoBarra { get; set; }=default!;
    public string NombreLibro { get; set; } = default!;
    public int Estado { get; set; }



    public DateTime FechaReserva { get; set; }
    public DateTime? FechaDevolucion { get; set; }
    public DateTime? FechaPrestamo { get; set; }
}