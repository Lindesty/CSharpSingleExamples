using _4_EFCoreWithSqliteInWPF.Database.MockData;
using _4_EFCoreWithSqliteInWPF.Models;
using Microsoft.EntityFrameworkCore;

namespace _4_EFCoreWithSqliteInWPF.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> optionBuilderOptions) :base(optionBuilderOptions)
    {
        
    }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(MockBookData.Books);
        base.OnModelCreating(modelBuilder);
    }
}