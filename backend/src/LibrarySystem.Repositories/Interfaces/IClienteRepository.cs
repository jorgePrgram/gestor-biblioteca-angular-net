using LibrarySystem.Entities;

namespace LibrarySystem.Repositories.Interfaces;

public interface IClienteRepository : IRepositoryBase<Cliente>
{

    Task<Cliente?> GetByUserIdAsync(string userId);
}