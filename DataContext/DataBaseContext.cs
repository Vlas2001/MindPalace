using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataContext
{
    public sealed class DataBaseContext: DbContext 
    {
        public DbSet<User> Users { get; set; }
        
        public DbSet<Statistics> Statistics { get; set; }
        
        public DbSet<MemorizeItem> MemorizeItems { get; set; }

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataBaseContext).Assembly);
        }
    }
}