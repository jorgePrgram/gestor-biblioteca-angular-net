using LibrarySystem.Entities;

namespace LibrarySystem.Repositories.Interfaces;

public interface IRepositoryBase<TEntity> where TEntity : EntityBase
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<TEntity?> GetAsync(int id);
    Task<int> AddAsync(TEntity entity);
    Task DeleteAsync(int id);
    Task UpdateAsync(TEntity entity);
}
