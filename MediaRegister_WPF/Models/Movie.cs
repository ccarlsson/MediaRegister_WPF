namespace MediaRegister_WPF.Models;
internal class Movie(string title, string director, int length) : Media(title)
{
    private string _director = director;
    private int _length = length;

    override public string ToString()
    {
        return $"{Title} by {_director}, {_length} minutes";
    }
}
