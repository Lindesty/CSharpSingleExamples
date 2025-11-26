using _4_EFCoreWithSqliteInWPF.Database.ConfigConstant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace _4_EFCoreWithSqliteInWPF.Database;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionBuilder.UseSqlite(DatabaseConstant.ConnectionString);
        
        return new AppDbContext(optionBuilder.Options);
    }
}