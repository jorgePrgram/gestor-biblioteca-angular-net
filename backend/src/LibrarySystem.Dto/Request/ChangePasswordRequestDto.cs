using System.Collections.Specialized;

namespace LibrarySystem.Dto.Request;

public class ChangePasswordRequestDto
{
    public String OldPassword { get; set; } = default!;
    public String NewPassword { get; set; }=default!;
}