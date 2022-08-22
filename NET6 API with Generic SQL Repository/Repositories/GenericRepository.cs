using Microsoft.EntityFrameworkCore;
using NET6_API_with_Generic_SQL_Repository.Data;
using NET6_API_with_Generic_SQL_Repository.Interfaces;

namespace NET6_API_with_Generic_SQL_Repository.Repositories;
public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DataContext context;
    public GenericRepository(DataContext context)
    {
        this.context = context;
    }
    public async Task<bool> AddAsync(TEntity entity)
    {
        try
        {
            await context.Set<TEntity>().AddAsync(entity);
            return true;
        }
        catch
        {
            return false;
        }

    }

    public async Task<bool> AddRangeAsync(IEnumerable<TEntity> entities)
    {

        try
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await context.Set<TEntity>().SingleOrDefaultAsync();
    }

    public bool Remove(TEntity entity)
    {
        try
        {
            context.Set<TEntity>().Remove(entity);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool RemoveRange(IEnumerable<TEntity> entities)
    {
        try
        {
            context.Set<TEntity>().RemoveRange(entities);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Update(TEntity entity)
    {
        try
        {
            context.Set<TEntity>().Update(entity);
            return true;
        }
        catch
        {
            return false;
        }
    }
}