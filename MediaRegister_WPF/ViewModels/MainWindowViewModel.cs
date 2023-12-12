using MediaRegister_WPF.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MediaRegister_WPF.ViewModels;
internal class MainWindowViewModel
{
    private readonly List<Media> _mediaRegister = new();

    public MainWindowViewModel()
    {
        AddBookCommand = new RelayCommand(AddBook, CanAddBook);
        AddMovieCommand = new RelayCommand(AddMovie, CanAddMovie);
        UpdateRadioButtonsCommand = new RelayCommand(UpdateRadioButtons);
    }

    public ICommand AddBookCommand { get; }
    public ICommand AddMovieCommand { get; }
    public ICommand UpdateRadioButtonsCommand { get; }

    public string BookTitle { get; set; } = "";
    public string BookAuthor { get; set; } = "";
    public int BookPages { get; set; }
    public string MovieTitle { get; set; } = "";
    public string MovieDirector { get; set; } = "";
    public int MovieLength { get; set; }
    public bool IsAllChecked { get; set; } = true;
    public bool IsBooksChecked { get; set; } = false;
    public bool IsMoviesChecked { get; set; } = false;
    public ObservableCollection<Media> MediaList { get; set; } = new();
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
          //  MessageBox.Show("Invalid input");
        }
        finally
        {
            BookTitle = "";
            BookAuthor = "";
            BookPages = 0;
        }
    }

    private void UpdateListBox()
    {
        MediaList.Clear();
        foreach (Media media in _mediaRegister)
        {
            if(IsAllChecked)
            {
                MediaList.Add(media);
            }
            else if(IsBooksChecked && media is Book)
            {
                MediaList.Add(media);
            }
            else if(IsMoviesChecked && media is Movie)
            {
                MediaList.Add(media);
            }
        }
    }

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
           // MessageBox.Show("Invalid input");
        }
        finally
        {
            MovieTitle = "";
            MovieDirector = "";
            MovieLength = 0;
        }
    } 

    private void UpdateRadioButtons()
    {
        UpdateListBox();
    }

    private bool CanAddBook()
    {
        return !string.IsNullOrWhiteSpace(BookTitle) && !string.IsNullOrWhiteSpace(BookAuthor) && BookPages > 0;
    }

    private bool CanAddMovie()
    {
        return !string.IsNullOrWhiteSpace(MovieTitle) && !string.IsNullOrWhiteSpace(MovieDirector) && MovieLength > 0;
    }
}