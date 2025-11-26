using System;
using System.Collections.Generic;
using _4_EFCoreWithSqliteInWPF.Models;

namespace _4_EFCoreWithSqliteInWPF.Database.MockData;

public static class MockBookData
{
    public static List<Book> Books =
    [
        new()
        {
            BookId = 1,
            BookName = "bookName1",
            AuthorName = "authorName1",
            Publisher = "publisher1",
            PublicationYearUtcTick = DateTimeOffset.MinValue.UtcTicks,
        },
        new()
        {
            BookId = 2,
            BookName = "bookName2",
            AuthorName = "authorName2",
            Publisher = "publisher2",
            PublicationYearUtcTick = DateTimeOffset.MinValue.UtcTicks,
        },
        new()
        {
            BookId = 3,
            BookName = "bookName3",
            AuthorName = "authorName3",
            Publisher = "publisher3",
            PublicationYearUtcTick = DateTimeOffset.MinValue.UtcTicks,
        },
        new()
        {
            BookId = 4,
            BookName = "bookName4",
            AuthorName = "authorName4",
            Publisher = "publisher4",
            PublicationYearUtcTick = DateTimeOffset.MinValue.UtcTicks,
        },
        new()
        {
            BookId = 5,
            BookName = "bookName5",
            AuthorName = "authorName5",
            Publisher = "publisher5",
            PublicationYearUtcTick = DateTimeOffset.MinValue.UtcTicks,
        },
        new()
        {
            BookId = 6,
            BookName = "bookName6",
            AuthorName = "authorName6",
            Publisher = "publisher6",
            PublicationYearUtcTick = DateTimeOffset.MinValue.UtcTicks,
        },
        new()
        {
            BookId = 7,
            BookName = "bookName7",
            AuthorName = "authorName7",
            Publisher = "publisher7",
            PublicationYearUtcTick = DateTimeOffset.MinValue.UtcTicks,
        },
        new()
        {
            BookId = 8,
            BookName = "bookName8",
            AuthorName = "authorName8",
            Publisher = "publisher8",
            PublicationYearUtcTick = DateTimeOffset.MinValue.UtcTicks,
        },
        new()
        {
            BookId = 9,
            BookName = "bookName9",
            AuthorName = "authorName9",
            Publisher = "publisher9",
            PublicationYearUtcTick = DateTimeOffset.MinValue.UtcTicks,
        },
    ];
}