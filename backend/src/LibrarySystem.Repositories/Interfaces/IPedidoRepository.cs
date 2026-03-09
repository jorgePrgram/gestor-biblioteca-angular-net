using LibrarySystem.Entities;
using LibrarySystem.Persistence.Migrations;

namespace LibrarySystem.Repositories.Interfaces;

public interface IPedidoRepository : IRepositoryBase<Pedido>
{
    Task<Pedido?> GetByIdWithEjemplaresAsync(int id);

    Task<List<int>> GetLibrosPrestadosAsync(int clienteId);
    Task<bool> ExistePrestamoActivoAsync(int clienteId, int libroId);

    Task<List<Pedido>> GetByClienteWhitEjemplaresAsync(int clienteId);
    Task<List<Pedido>> GetAllWithEjemplaresAsync();
    Task ExecuteTransactionAsync(Func<Task> operation);
}