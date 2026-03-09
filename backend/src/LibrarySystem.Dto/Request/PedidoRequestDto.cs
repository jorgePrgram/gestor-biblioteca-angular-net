namespace LibrarySystem.Dto.Request;

public class PedidoRequestDto
{
    public List<int> LibrosIds { get; set; } = new();
    public List<string> CodigosBarra { get; set; } = new();
}