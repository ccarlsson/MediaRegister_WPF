using MediaRegister_WPF.Models;
using System.Windows;

namespace MediaRegister_WPF;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly List<Media> _mediaRegister = new();

    public MainWindow()
    {
        InitializeComponent();
        rbAll.IsChecked = true;
    }

    private void AddBookClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string title = tbxBookTitle.Text;
            string author = tbxBookAuthor.Text;
            int pages = int.Parse(tbxBookPages.Text);

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
            tbxBookTitle.Clear();
            tbxBookAuthor.Clear();
            tbxBookPages.Clear();
        }

    }

    private void AddMovieClick(object sender, RoutedEventArgs e)
    {
        try
        {
            string title = tbxMoveTitle.Text;
            string director = tbxMovieDirector.Text;
            int length = int.Parse(tbxMovieLength.Text);

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
            tbxMoveTitle.Clear();
            tbxMovieDirector.Clear();
            tbxMovieLength.Clear();
        }
    }

    private void RadioButtonChecked(object sender, RoutedEventArgs e)
    {
        UpdateListBox();
    }

    private void UpdateListBox()
    {
        listbox.Items.Clear();
        foreach (Media media in _mediaRegister)
        {
            if (rbMovies.IsChecked == true && media is Movie)
            {
                listbox.Items.Add(media);
            }
            else if (rbBooks.IsChecked == true && media is Book)
            {
                listbox.Items.Add(media);
            }
            else if (rbAll.IsChecked == true)
            {
                listbox.Items.Add(media);
            }
        }
    }
}