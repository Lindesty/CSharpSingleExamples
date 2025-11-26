using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using _4_EFCoreWithSqliteInWPF.Extensions;
using _4_EFCoreWithSqliteInWPF.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using _4_EFCoreWithSqliteInWPF.Services;
using _4_EFCoreWithSqliteInWPF.ViewModels;
using _4_EFCoreWithSqliteInWPF.Database.MockData;
using MapsterMapper;

namespace _4_EFCoreWithSqliteInWPF;

public class MainViewModel : ViewModelBase
{
    private readonly ILogger<MainViewModel> _logger;
    private readonly IBookRepository _bookRepository;
    public MainViewModel(ILogger<MainViewModel> logger,  IBookRepository bookRepository)
    {
        _logger = logger;
        _bookRepository = bookRepository;

        InitCommand = new RelayCommand(Init);
    }


    public ICommand InitCommand { get; }
    private void Init()
    {
        LoadBooksFromDatabase();
        FilterBook();
    }


    public void LoadBooksFromDatabase()
    {
        BooKs.AddRange(_bookRepository.GetAllBookForShows());
        BooKs.ForEach(book => book.StartListenModifyEvent());

    }



    public string Title
    {
        get;
        set => SetProperty(ref field, value);
    } = "BookManager";

    public string? SearchText
    {
        get;
        set => SetProperty(ref field, value).Then(FilterBook);
    } = null;

    private void FilterBook()
    {
        var searchText = SearchText;
        FilteredBooks.Clear();
        if (string.IsNullOrWhiteSpace(searchText))
        {
            foreach (var book in BooKs) FilteredBooks.Add(book);
        }
        else
        {
            var keywords = searchText.Split(' ');

            bool ContainsKeyWords(string value)
            {
                foreach (var keyword in keywords)
                {
                    if (!value.Contains(keyword)) return false;
                }

                return true;
            }

            BooKs.Where(book => ContainsKeyWords(book.BookName))
                .ForEach(book => FilteredBooks.Add(book));
        }
    }

    private List<BookForShow> BooKs { get; } = new();

    public BookForShow? SelectedBook
    {
        get;
        set => SetProperty(ref field, value);
    }

    public ObservableCollection<BookForShow> FilteredBooks { get; } = new();
    public ICommand CreateNewBookCommand => new RelayCommand(() =>
    {
        var bookForAdd = new BookForShow();
        bookForAdd.BookName = "untitled bookName";
        bookForAdd.AuthorName = "untitled authorName";
        bookForAdd.Publisher = "untitled publisher";
        bookForAdd.PublicationYearUtcTick = DateTimeOffset.UtcNow.UtcTicks;
        BooKs.Add(bookForAdd);
        FilteredBooks.Add(bookForAdd);
        SelectedBook = bookForAdd;
        var result = _bookRepository.AddOrUpdateBook(bookForAdd)  ? "成功" : "失败";
        MessageBox.Show($"添加书本{result}");
    });
    public ICommand DeleteBookCommand => new RelayCommand(() =>
    {
        var selectedBook = SelectedBook;
        if (selectedBook is null) return;
        _bookRepository.DeleteBook(selectedBook);
        BooKs.Remove(selectedBook);
        FilteredBooks.Remove(selectedBook);
        SelectedBook = null;
    });
    public ICommand FlashBookCommand => new RelayCommand(() =>
    {
        this.BooKs.Clear();
        var books = _bookRepository.GetAllBookForShows();
        this.BooKs.AddRange(books);
        SearchText = string.Empty;
    });
    public ICommand SubmitBookChangeCommand => new RelayCommand(() =>
    {
        foreach (var book in BooKs.Where(book => book.IsModified))
        {
            _bookRepository.AddOrUpdateBook(book);
            book.IsModified = false;
        }
    });
}

public class BookForShow : ObservableObject
{
    public long BookId
    {
        get;
        set => SetProperty(ref field, value);
    }

    public string BookName
    {
        get;
        set => SetProperty(ref field, value);
    } = string.Empty;

    public string AuthorName
    {
        get;
        set => SetProperty(ref field, value);
    } = string.Empty;

    public string Publisher
    {
        get;
        set => SetProperty(ref field, value);
    } = string.Empty;

    public long PublicationYearUtcTick
    {
        get;
        set => SetProperty(ref field, value);
    }


    public bool IsModified
    {
        get;
        set => SetProperty(ref field, value);
    } = false;

    public void StartListenModifyEvent() => this.PropertyChanged += ChangeModifiedState;
    private void ChangeModifiedState(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(BookForShow.IsModified)) return;
        this.IsModified = true;
    }
}