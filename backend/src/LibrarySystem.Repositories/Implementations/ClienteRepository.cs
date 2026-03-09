using LibrarySystem.Entities;
using LibrarySystem.Persistence;
using LibrarySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositories.Implementations;

public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
{
    public ClienteRepository(AppDbContext context) : base(context)
    {
     

    }

    public async Task<Cliente?> GetByUserIdAsync(string userId)
    {
        return await context.Set<Cliente>()
            .FirstOrDefaultAsync(x => x.UserId == userId);
    }
}