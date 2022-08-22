using NET6_API_with_Generic_SQL_Repository.Entities;

namespace NET6_API_with_Generic_SQL_Repository.Interfaces;

public interface IUnitOfWork 
{
        IGenericRepository<User> Users { get; }
     IGenericRepository<ExampleEntity> Examples { get; }
    Task<bool> Complete();
        public void Dispose();

}