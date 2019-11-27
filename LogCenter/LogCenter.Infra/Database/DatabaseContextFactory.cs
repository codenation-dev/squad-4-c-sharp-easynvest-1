using LogCenter.Infra.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseSqlite(@"Filename=..\LogCenter.db");
        return new DatabaseContext(builder.Options);
    }
}