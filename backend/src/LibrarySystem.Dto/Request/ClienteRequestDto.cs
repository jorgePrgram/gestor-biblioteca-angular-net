namespace LibrarySystem.Dto.Request;

public class ClienteRequestDto
{

    public string Nombre { get; set; }
    public string Email { get; set; }
    public string Apellido { get; set; }
    public int Edad { get; set; }
    public int Dni { get; set; }
    public string UserId { get; set; }
}