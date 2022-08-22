namespace NET6_API_with_Generic_SQL_Repository.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<bool> AddAsync(TEntity entity);
    Task<bool> AddRangeAsync(IEnumerable<TEntity> entities);

    bool Remove(TEntity entity);
    bool RemoveRange(IEnumerable<TEntity> entities);

    bool Update(TEntity entity);



}