using LibrarySystem.Dto.Response;
using LibrarySystem.Persistence;
using LibrarySystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using LibrarySystem.Services.Configuration;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace LibrarySystem.Services.Implementations;

public class JwtTokenService : IJwtTokenService { 
    private readonly UserManager<LibrarySystemUserIdentity> userManager; 
    private readonly JwtSettings jwt; 
    public JwtTokenService(UserManager<LibrarySystemUserIdentity> userManager, IOptions<JwtSettings> options) { 
        this.userManager = userManager; 
        jwt = options.Value; } 
    public async Task<LoginResponseDto> GenerateTokenAsync(LibrarySystemUserIdentity user) { 
        var claims = new List<Claim> { 
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, $"{user.FirstName} { user.LastName}")
        };
    

        var roles = await userManager.GetRolesAsync(user); 

        foreach (var role in roles) { 
            claims.Add(new Claim(ClaimTypes.Role, role)); 
        } 

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.JWTKey)); 
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); 
        var expiration = DateTime.UtcNow.AddSeconds(jwt.LifetimeInSeconds); 
        var token = new JwtSecurityToken(claims: 
            claims, 
            expires: expiration, 
            signingCredentials: creds); 
        return new LoginResponseDto 
        { Token = new JwtSecurityTokenHandler().WriteToken(token), 
            ExpirationDate = expiration }; 
    } 
}