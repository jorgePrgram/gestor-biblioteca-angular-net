namespace LibrarySystem.Entities;

public class Cliente: EntityBase
{
    public string Nombre { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Apellido { get; set; } = default!;
    public string DNI { get; set; } = default!;
    public int Edad { get; set; }

    public string UserId { get; set; }=default!;
}