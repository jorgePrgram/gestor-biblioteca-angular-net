using LibrarySystem.Entities;
using LibrarySystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Repositories.Implementations;

public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
{
    protected readonly DbContext context;
    protected RepositoryBase(DbContext context)
    {
        this.context = context;
    }

    public async Task<int> AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var item = await GetAsync(id);
        if (item is not null)
        {
            item.Status = false;
            await context.SaveChangesAsync();
        }
    }

    public virtual async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>()
             .Where(x => x.Status == true)
            .AsNoTracking()
            .ToListAsync();
    }

    public virtual async Task<TEntity?> GetAsync(int id)
    {
        return await context.Set<TEntity>()
             .FirstOrDefaultAsync(x => x.Id == id && x.Status == true);
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
        await context.SaveChangesAsync();
    }
}