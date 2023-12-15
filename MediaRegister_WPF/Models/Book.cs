namespace MediaRegister_WPF.Models;
internal record Book(int Id, string Title, string Author, int Pages) : Media(Id, Title)
{

    override public string ToString()
    {
        return $"{Title} by {Author}, {Pages} pages";
    }
}
