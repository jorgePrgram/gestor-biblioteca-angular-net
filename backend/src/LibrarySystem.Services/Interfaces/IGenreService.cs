using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;

namespace LibrarySystem.Services.Interfaces;

public interface IGenreService 
{
    Task<BaseResponseGeneric<ICollection<GenreResponseDto>>> GetAllAsync();
    Task<BaseResponseGeneric<GenreResponseDto>> GetAsync(int id);
    Task<BaseResponseGeneric<int>> AddAsync(GenreRequestDto request);
    Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request);
    Task<BaseResponse> DeleteAsync(int id);
}