using LibrarySystem.Dto.Response;
using LibrarySystem.Persistence;

namespace LibrarySystem.Services.Interfaces;

public interface IJwtTokenService
{
    Task<LoginResponseDto> GenerateTokenAsync(LibrarySystemUserIdentity user);
}