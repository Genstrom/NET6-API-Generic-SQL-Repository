using Microsoft.EntityFrameworkCore;
using NET6_API_with_Generic_SQL_Repository.Entities;

namespace NET6_API_with_Generic_SQL_Repository.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
    }

}