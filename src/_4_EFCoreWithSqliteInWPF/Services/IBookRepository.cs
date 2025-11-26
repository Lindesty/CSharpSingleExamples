using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _4_EFCoreWithSqliteInWPF.Database;
using _4_EFCoreWithSqliteInWPF.Models;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace _4_EFCoreWithSqliteInWPF.Services;

public interface IBookRepository
{
    IEnumerable<BookForShow> GetAllBookForShows();
    bool AddOrUpdateBook(BookForShow book);
    bool DeleteBook(BookForShow book);
    bool AddOrUpdateBooks(IEnumerable<BookForShow> bookForShows);
}

public class BookRepository : IBookRepository
{
    private readonly IDbContextFactory<AppDbContext> _appDbContentFactory;
    private readonly IMapper _mapper;
    
    public BookRepository(IDbContextFactory<AppDbContext> appDbContentFactory, IMapper mapper)
    {
        _appDbContentFactory = appDbContentFactory;
        _mapper = mapper;
    }

    public IEnumerable<BookForShow> GetAllBookForShows()
    {
        using var context = _appDbContentFactory.CreateDbContext();
        var books = context.Books.ToList();
        return _mapper.Map<IEnumerable<BookForShow>>(books);
    }

    public bool AddOrUpdateBook(BookForShow book)
    {
        try
        {
            using var context = _appDbContentFactory.CreateDbContext();
            var bookEntity = _mapper.Map<Book>(book);

            if (book.BookId == 0)
            {
                context.Books.Add(bookEntity);
            }
            else
            {
                var existingBook = context.Books.Find(book.BookId);
                if (existingBook != null)
                {
                    context.Entry(existingBook).CurrentValues.SetValues(bookEntity);
                }
                else
                {
                    context.Books.Add(bookEntity);
                }
            }

            context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteBook(BookForShow book)
    {
        try
        {
            using var context = _appDbContentFactory.CreateDbContext();
            var bookEntity = context.Books.Find(book.BookId);

            if (bookEntity != null)
            {
                context.Books.Remove(bookEntity);
                context.SaveChanges();
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool AddOrUpdateBooks(IEnumerable<BookForShow> bookForShows)
    {
        try
        {
            using var context = _appDbContentFactory.CreateDbContext();

            foreach (var bookForShow in bookForShows)
            {
                var bookEntity = _mapper.Map<Book>(bookForShow);

                if (bookForShow.BookId == 0)
                {
                    context.Books.Add(bookEntity);
                }
                else
                {
                    var existingBook = context.Books.Find(bookForShow.BookId);
                    if (existingBook != null)
                    {
                        context.Entry(existingBook).CurrentValues.SetValues(bookEntity);
                    }
                    else
                    {
                        context.Books.Add(bookEntity);
                    }
                }
            }

            context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }
}