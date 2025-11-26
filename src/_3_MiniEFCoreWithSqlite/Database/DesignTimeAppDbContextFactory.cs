using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using MiniEFCoreWithSqliteTest.Database.Config;

namespace MiniEFCoreWithSqliteTest.Database;

public class DesignTimeAppDbContextFactory :IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionBuilder.UseSqlite(DbConfigConstant.DbConnectionString);
        return new AppDbContext(optionBuilder.Options);
    }
}