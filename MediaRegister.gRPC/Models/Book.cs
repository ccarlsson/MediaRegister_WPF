namespace MediaRegister.gRPC.Models;

public record Book(string Title, string Author, int Pages)
{
    public int Id { get; set; }
}
