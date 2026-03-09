using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LibrarySystem.Persistence;

public class LibrarySystemUserIdentity :IdentityUser
{
    [StringLength(100)]
    public string FirstName { get; set; } = default!;

    [StringLength(100)]
    public string LastName { get; set; } = default!;
}