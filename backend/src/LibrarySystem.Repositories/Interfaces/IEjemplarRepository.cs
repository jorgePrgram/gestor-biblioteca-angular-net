using LibrarySystem.Entities;

namespace LibrarySystem.Repositories.Interfaces;

public interface IEjemplarRepository : IRepositoryBase<Ejemplar>
{
    Task<Ejemplar?> GetByCodigoBarraAsync(string codigoBarra);
    Task<Ejemplar?> GetDisponibleByLibroAsync(int libroId);   
    Task<List<Ejemplar>> GetDisponiblesByLibroIdAsync(int libroId);
}