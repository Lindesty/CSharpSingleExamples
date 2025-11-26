using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MiniEFCoreWithSqliteTest.Database.MockData;
using MiniEFCoreWithSqliteTest.Models;

namespace MiniEFCoreWithSqliteTest.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Book>().HasData(MockBookData.Books);


        base.OnModelCreating(modelBuilder); 
    }
}