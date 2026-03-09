using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;

namespace LibrarySystem.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponseGeneric<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request);
    Task<BaseResponseGeneric<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    Task<BaseResponse> RequestTokenResetPasswordAsync(ResetPasswordRequestDto request);
    Task<BaseResponse> ResetPasswordAsync(NewPasswordRequestDto request);
    Task<BaseResponse> ChangePasswordAsync(string email, ChangePasswordRequestDto request);


}