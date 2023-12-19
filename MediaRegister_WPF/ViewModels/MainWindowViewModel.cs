using MediaRegister_WPF.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MediaRegister_WPF.ViewModels;
internal class MainWindowViewModel : BaseViewModel
{
    // A list to store all media objects
    private readonly List<Media> _mediaRegister = [];

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
    private string _bookTitle = "";
    private string _bookAuthor = "";
    private int _bookPages = 0;
    public string BookTitle {
        get
        {
            return _bookTitle;
        } 
        set
        {
            SetProperty(ref _bookTitle, value, nameof(BookTitle));
        }
    }
    public string BookAuthor
    {
        get
        {
            return _bookAuthor;
        }
        set
        {
            SetProperty(ref _bookAuthor, value, nameof(BookAuthor));
        }
    }
    public int BookPages
    {
        get
        {
            return _bookPages;
        }
        set
        {
            SetProperty(ref _bookPages, value, nameof(BookPages));
        }
    }


    private string _movieTitle = "";
    private string _movieDirector = "";
    private int _movieLength = 0;
    public string MovieTitle
    {
        get { return _movieTitle; }
        set { SetProperty(ref _movieTitle, value, nameof(MovieTitle)); }
    }
    public string MovieDirector { get { return _movieDirector; } set { SetProperty(ref _movieDirector, value, nameof(MovieDirector)); } }
    public int MovieLength { get { return _movieLength; } set { SetProperty(ref _movieLength, value, nameof(MovieLength)); } }

    // Properties for radio button states
    public bool IsAllChecked { get; set; } = true;
    public bool IsBooksChecked { get; set; } = false;
    public bool IsMoviesChecked { get; set; } = false;

    // Observable collection for the media list
    public ObservableCollection<Media> MediaList { get; set; } = [];

    // Method to add a book to the media register
    private void AddBook()
    {

        string title = BookTitle;
        string author = BookAuthor;
        int pages = BookPages;
        Book book = new(title, author, pages);
        _mediaRegister.Add(book);
        UpdateListBox();
        BookTitle = "";
        BookAuthor = "";
        BookPages = 0;

    }


    // Method to add a movie to the media register
    private void AddMovie()
    {
        string title = MovieTitle;
        string director = MovieDirector;
        int length = MovieLength;
        Movie movie = new(title, director, length);
        _mediaRegister.Add(movie);
        UpdateListBox();
        MovieTitle = "";
        MovieDirector = "";
        MovieLength = 0;
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
