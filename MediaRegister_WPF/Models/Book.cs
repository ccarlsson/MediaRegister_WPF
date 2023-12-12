namespace MediaRegister_WPF.Models;
internal class Book(string title, string author, int pages) : Media(title)
{
    private string _author = author;
    private int _pages = pages;

    override public string ToString()
    {
        return $"{Title} by {_author}, {_pages} pages";
    }
}
