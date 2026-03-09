using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;


namespace LibrarySystem.Services.Interfaces;

public interface IClienteService
{
    Task<BaseResponseGeneric<ICollection<ClienteResponseDto>>> GetAsync();
    Task<BaseResponseGeneric<int>> AddAsync(ClienteRequestDto request);
    Task<BaseResponse> UpdateAsync(int id, ClienteRequestDto request);
    Task<BaseResponse> DeleteAsync(int id);
}