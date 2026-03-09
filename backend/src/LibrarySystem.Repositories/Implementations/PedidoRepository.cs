using LibrarySystem.Entities;
using LibrarySystem.Entities.Enums;
using LibrarySystem.Persistence;
using LibrarySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositories.Implementations;

public class PedidoRepository : RepositoryBase<Pedido>, IPedidoRepository
{
    public PedidoRepository(AppDbContext context) : base(context)
    {

    }
    public async Task<Pedido?> GetByIdWithEjemplaresAsync(int id)
    {
        return await context.Set<Pedido>()
             .Include(p => p.Cliente)
        .Include(p => p.PedidoEjemplares)
            .ThenInclude(pe => pe.Ejemplar)
                .ThenInclude(e => e.Libro)
        .FirstOrDefaultAsync(p => p.Id == id && p.Status == true);

    }

    public async Task<List<int>> GetLibrosPrestadosAsync(int clienteId)
    {
        return await context.Set<PedidoEjemplar>()
            .Include(pe => pe.Ejemplar)
            .Include(pe => pe.Pedido)
            .Where(pe => pe.Pedido.ClienteId == clienteId && (pe.Ejemplar.Estado == EstadoEjemplar.Prestado ||
            pe.Ejemplar.Estado == EstadoEjemplar.Reservado))
            .Select(pe=> pe.Ejemplar.LibroId)
            .Distinct()
            .ToListAsync();
    }

    public async Task<bool> ExistePrestamoActivoAsync(int clienteId, int libroId)
    {
        return await context.Set<Pedido>()
            .Where(p => p.ClienteId == clienteId && p.Status == true)
            .SelectMany(p => p.PedidoEjemplares)
            .AnyAsync(pe =>
                pe.Ejemplar.LibroId == libroId &&
                (pe.Ejemplar.Estado == EstadoEjemplar.Prestado ||
             pe.Ejemplar.Estado == EstadoEjemplar.Reservado)
            );
    }

    public async Task<List<Pedido>> GetByClienteWhitEjemplaresAsync(int clienteId)
    {
        return await context.Set<Pedido>()
            .Where(p=> p.ClienteId == clienteId)
            .Include(p=> p.Cliente)
            .Include(p=>p.PedidoEjemplares).ThenInclude(pe=>pe.Ejemplar)
            .ThenInclude(e=>e.Libro).ToListAsync();  
    }

    public async Task<List<Pedido>> GetAllWithEjemplaresAsync()
    {
        return await context.Set<Pedido>().
            Include(p=> p.Cliente)
            .Include(p=>p.PedidoEjemplares)
            .ThenInclude(pe => pe.Ejemplar)
            .ThenInclude(e=>e.Libro)
            .ToListAsync();
    }


    public async Task ExecuteTransactionAsync(Func<Task> operation)
    {
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            await operation();
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


}