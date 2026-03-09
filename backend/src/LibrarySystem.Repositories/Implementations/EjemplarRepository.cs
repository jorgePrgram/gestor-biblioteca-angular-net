using LibrarySystem.Entities;
using LibrarySystem.Entities.Enums;
using LibrarySystem.Persistence;
using LibrarySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositories.Implementations;

public class EjemplarRepository : RepositoryBase<Ejemplar>, IEjemplarRepository 
{
    public EjemplarRepository(AppDbContext context) : base(context) { 
    
    }

    public async Task<Ejemplar?> GetByCodigoBarraAsync(string codigoBarra)
    {
        return await context.Set<Ejemplar>()
            .Include(e => e.Libro)
            .FirstOrDefaultAsync(e => e.CodigoBarra == codigoBarra);
    }

    public async Task<Ejemplar?> GetDisponibleByLibroAsync(int libroId)
    {
        return await context.Set<Ejemplar>()
            .Include(e => e.Libro)
            .Where(e => e.LibroId == libroId && e.Estado == EstadoEjemplar.Disponible)
            .FirstOrDefaultAsync();
    }


    public async Task<List<Ejemplar>> GetDisponiblesByLibroIdAsync(int libroId)
    {
        return await context.Set<Ejemplar>()
            .Where(e => e.LibroId == libroId
                     && e.Estado == EstadoEjemplar.Disponible)
            .AsNoTracking()
            .ToListAsync();
    }
}