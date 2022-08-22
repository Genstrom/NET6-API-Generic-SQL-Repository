using NET6_API_with_Generic_SQL_Repository.Entities;
using NET6_API_with_Generic_SQL_Repository.Interfaces;

namespace NET6_API_with_Generic_SQL_Repository.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    public UnitOfWork(DataContext context)
    {
        _context = context;
    }

    public IGenericRepository<User> Users { get; }
    public IGenericRepository<ExampleEntity> Examples { get; }
    public async Task<bool> Complete()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}