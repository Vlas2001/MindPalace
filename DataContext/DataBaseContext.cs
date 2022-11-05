using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public sealed class DataBaseContext: DbContext 
    {
        public DbSet<User> Users { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}