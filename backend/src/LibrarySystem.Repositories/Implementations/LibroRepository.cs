using LibrarySystem.Entities;
using LibrarySystem.Persistence;
using LibrarySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositories.Implementations;

public class LibroRepository : RepositoryBase<Libro>, ILibroRepository
{
    public LibroRepository(AppDbContext context) : base(context)
    {
    }

 
    public override async Task<ICollection<Libro>> GetAllAsync()
    {
        return await context.Set<Libro>().Include(x=>x.Genre)
            .Include(x=>x.Ejemplares)
            .Where(x => x.Status == true)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<ICollection<Libro>> GetByGenreAsync(int genreId)
    {
        return await context.Set<Libro>().Where(l => l.GenreId == genreId)
            .ToListAsync();
    }

    public override async Task<Libro?> GetAsync(int id)
    {
        return await context.Set<Libro>()
            .Include(l => l.Genre)
            .Include(l => l.Ejemplares)
            .FirstOrDefaultAsync(l=>l.Id== id && l.Status==true);
    }
}