namespace LibrarySystem.Services.Configuration;

public class JwtSettings
{
    public string JWTKey { get; set; } = default!;
    public int LifetimeInSeconds { get; set; }
}
