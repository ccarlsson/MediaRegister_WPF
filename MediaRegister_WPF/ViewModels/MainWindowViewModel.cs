using MediaRegister_WPF.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MediaRegister_WPF.ViewModels;
internal class MainWindowViewModel
{
    // A list to store all media objects
    private readonly List<Media> _mediaRegister = new();

    // Constructor initializes all commands
    public MainWindowViewModel()
    {
        AddBookCommand = new RelayCommand(AddBook, CanAddBook);
        AddMovieCommand = new RelayCommand(AddMovie, CanAddMovie);
        UpdateRadioButtonsCommand = new RelayCommand(UpdateRadioButtons);
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
    public ObservableCollection<Media> MediaList { get; set; } = new();

    // Method to add a book to the media register
    private void AddBook()
    {
        try
        {
            string title = BookTitle;
            string author = BookAuthor;
            int pages = BookPages;
            Book book = new(title, author, pages);
            _mediaRegister.Add(book);
            UpdateListBox();
        }
        catch
        {
            MessageBox.Show("Invalid input");
        }
        finally
        {
            BookTitle = "";
            BookAuthor = "";
            BookPages = 0;
        }
    }


    // Method to add a movie to the media register
    private void AddMovie()
    {
        try
        {
            string title = MovieTitle;
            string director = MovieDirector;
            int length = MovieLength;
            Movie movie = new(title, director, length);
            _mediaRegister.Add(movie);
            UpdateListBox();
        }
        catch
        {
            MessageBox.Show("Invalid input");
        }
        finally
        {
            MovieTitle = "";
            MovieDirector = "";
            MovieLength = 0;
        }
    }


    // Method to update the media list based on the selected radio button
    private void UpdateListBox()
    {
        MediaList.Clear();
        foreach (Media media in _mediaRegister)
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
