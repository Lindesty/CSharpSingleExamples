// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiniEFCoreWithSqliteTest.Database;
using MiniEFCoreWithSqliteTest.Database.Config;
using MiniEFCoreWithSqliteTest.Models;

IServiceCollection sc = new ServiceCollection();
sc.AddLogging(builder =>
{
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Information);
});

sc.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(DbConfigConstant.DbConnectionString);
});


var sp = sc.BuildServiceProvider();


using var dbContext = sp.GetRequiredService<AppDbContext>();
var logger = sp.GetRequiredService<ILogger<Program>>();

dbContext.Database.Migrate();



dbContext.Books.Add(new Book()
{
    BookName = "bookName" + Guid.NewGuid(),
    AuthorName = "test",
    Publisher = "unknown",
    PublicationYearUtcTick = DateTimeOffset.UtcNow.UtcTicks,
});

dbContext.SaveChanges();


var bookNames = dbContext.Books.Select(book=>book.BookName).ToList();

logger.LogInformation("Books:");
logger.LogInformation(string.Join('\n',bookNames));
logger.LogInformation("End");

