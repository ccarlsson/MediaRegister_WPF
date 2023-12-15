﻿using MediaRegister_WPF.Models;
using MediaRegister_WPF.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MediaRegister_WPF.ViewModels;
internal class MainWindowViewModel
{
    // A list to store all media objects
    // private readonly List<Media> _mediaRegister = [];
    private readonly MediaService _mediaService = new();

    // Constructor initializes all commands
    public MainWindowViewModel()
    {
        AddBookCommand = new RelayCommand(AddBook, CanAddBook);
        AddMovieCommand = new RelayCommand(AddMovie, CanAddMovie);
        UpdateRadioButtonsCommand = new RelayCommand(UpdateRadioButtons);

        UpdateListBox();
    }

    

    // Commands for adding a book, adding a movie, and updating radio buttons
    public ICommand AddBookCommand { get; }
    public ICommand AddMovieCommand { get; }
    public ICommand UpdateRadioButtonsCommand { get; }

    // Properties for book and movie details
    public string BookTitle { get; set; } = "";
    public string BookAuthor { get; set; } = "";
    public int BookPages { get; set; }
    public string MovieTitle { get; set; } = "";
    public string MovieDirector { get; set; } = "";
    public int MovieLength { get; set; }

    // Properties for radio button states
    public bool IsAllChecked { get; set; } = true;
    public bool IsBooksChecked { get; set; } = false;
    public bool IsMoviesChecked { get; set; } = false;

    // Observable collection for the media list
    public ObservableCollection<Models.Media> MediaList { get; set; } = [];

    // Method to add a book to the media register
    private async void AddBook()
    {

        string title = BookTitle;
        string author = BookAuthor;
        int pages = BookPages;
        Book book = new(0, title, author, pages);
        var newId = await _mediaService.AddBook(book);
        if(newId >= 0)
        {
            UpdateListBox();
            BookTitle = "";
            BookAuthor = "";
            BookPages = 0;
        }
        else
        {
            MessageBox.Show("Error adding book");
        }

    }


    // Method to add a movie to the media register
    private async void AddMovie()
    {
        string title = MovieTitle;
        string director = MovieDirector;
        int length = MovieLength;
        Movie movie = new(0, title, director, length);
        var newId = await _mediaService.AddMovie(movie);
        if(newId >= 0)
        {
            UpdateListBox();
            MovieTitle = "";
            MovieDirector = "";
            MovieLength = 0;
        }
        else
        {
            MessageBox.Show("Error adding movie");
        } 
    }


    // Method to update the media list based on the selected radio button
    private async void UpdateListBox()
    {
        MediaList.Clear();
        var mediaList = await _mediaService.GetAllMedia();
        foreach (Models.Media media in mediaList)
        {
            if (IsAllChecked)
            {
                MediaList.Add(media);
            }
            else if (IsBooksChecked && media is Book)
            {
                MediaList.Add(media);
            }
            else if (IsMoviesChecked && media is Movie)
            {
                MediaList.Add(media);
            }
        }
    }


    // Method to update the media list when a radio button is selected
    private void UpdateRadioButtons()
    {
        UpdateListBox();
    }

    // Method to check if a book can be added
    private bool CanAddBook()
    {
        return !string.IsNullOrWhiteSpace(BookTitle)
            && !string.IsNullOrWhiteSpace(BookAuthor)
            && BookPages > 0;
    }

    // Method to check if a movie can be added
    private bool CanAddMovie()
    {
        return !string.IsNullOrWhiteSpace(MovieTitle)
            && !string.IsNullOrWhiteSpace(MovieDirector)
            && MovieLength > 0;
    }
}
