using LibrarySystem.Dto.Request;
using LibrarySystem.Dto.Response;

namespace LibrarySystem.Services.Interfaces;

public interface ILibroService 
{
    Task<BaseResponseGeneric<ICollection<LibroResponseDto>>> GetAllAsync();
    Task<BaseResponseGeneric<LibroResponseDto>> GetAsync(int id);
    Task<BaseResponseGeneric<int>> AddAsync(LibroRequestDto request);
    Task<BaseResponse> UpdateAsync(int id, LibroRequestDto request);
    Task<BaseResponse> DeleteAsync(int id);
    Task<BaseResponseGeneric<ICollection<LibroResponseDto>>> GetByGenreAsync(int genreId);
    Task<BaseResponseGeneric<ICollection<EjemplarResponseDto>>> GetDisponiblesByLibroIdAsync(int libroId);


}