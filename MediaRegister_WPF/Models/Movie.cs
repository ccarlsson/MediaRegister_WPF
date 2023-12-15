namespace MediaRegister_WPF.Models;
internal record Movie(int Id, string Title, string Director, int Length) : Media(Id, Title)
{
    
    override public string ToString()
    {
        return $"{Title} by {Director}, {Length} minutes";
    }
}
