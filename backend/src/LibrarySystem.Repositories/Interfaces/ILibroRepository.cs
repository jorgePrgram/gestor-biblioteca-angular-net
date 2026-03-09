using LibrarySystem.Entities;

namespace LibrarySystem.Repositories.Interfaces;

public interface ILibroRepository: IRepositoryBase<Libro>
{
    Task<ICollection<Libro>> GetByGenreAsync(int genreId);
}