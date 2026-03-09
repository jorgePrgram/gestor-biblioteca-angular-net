namespace LibrarySystem.Dto.Request;

public class RegisterRequestDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int DocumentNumber {  get; set; }
    public int Edad { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

}