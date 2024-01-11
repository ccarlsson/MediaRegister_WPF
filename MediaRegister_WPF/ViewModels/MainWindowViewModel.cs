using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using MediaRegister.Client.Models;
using MediaRegister.Client.Services;

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
        DeleteCommand = new RelayCommand(DeleteMedia, CanDeleteMedia);

        UpdateListBox();
    }

    private void DeleteMedia()
    {
        if (MessageBox.Show($"Are you sure you want to delete {SelectedMedia.Title}?", "Delete media", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
        {
            if (SelectedMedia is Book)
            {
                _mediaService.DeleteBook(SelectedMedia.Id);
            }
            else if (SelectedMedia is Movie)
            {
                _mediaService.DeleteMovie(SelectedMedia.Id);
            }
            UpdateListBox();
        }
    }



    // Commands for adding a book, adding a movie, and updating radio buttons
    public ICommand AddBookCommand { get; }
    public ICommand AddMovieCommand { get; }
    public ICommand UpdateRadioButtonsCommand { get; }
    public ICommand DeleteCommand { get; }

    // Properties for book and movie details
    public string BookTitle { get; set; } = "";
    public string BookAuthor { get; set; } = "";
    public int BookPages { get; set; }
    public string MovieTitle { get; set; } = "";
    public string MovieDirector { get; set; } = "";
    public int MovieLength { get; set; }

    public MediaRegister.Client.Models.Media? SelectedMedia { get; set; }

    // Properties for radio button states
    public bool IsAllChecked { get; set; } = true;
    public bool IsBooksChecked { get; set; } = false;
    public bool IsMoviesChecked { get; set; } = false;

    // Observable collection for the media list
    public ObservableCollection<MediaRegister.Client.Models.Media> MediaList { get; set; } = [];

    // Method to add a book to the media register
    private async void AddBook()
    {

        string title = BookTitle;
        string author = BookAuthor;
        int pages = BookPages;
        Book book = new(0, title, author, pages);
        var newId = await _mediaService.AddBook(book);
        if (newId >= 0)
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
        if (newId >= 0)
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
        foreach (MediaRegister.Client.Models.Media media in mediaList)
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

    private bool CanDeleteMedia()
    {
        return SelectedMedia is not null;
    }
}
