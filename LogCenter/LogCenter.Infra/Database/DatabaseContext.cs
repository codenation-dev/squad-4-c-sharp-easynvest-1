using LogCenter.Domain.Entities;
using LogCenter.Infra.Configuration;
using Microsoft.EntityFrameworkCore;

namespace LogCenter.Infra.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new LogConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
