using MediaRegister.Client.Services;
using MediaRegister.Client.Models;

namespace MediaRegister.Client.Extensions;
internal static class MediaExtensions
{
    public static Book ToBook(this GetBookResponse response)
    {
        return new Book(response.Id, response.Title, response.Author, response.Pages);
    }

    public static Movie ToMovie(this GetMovieResponse response)
    {
        return new Movie(response.Id, response.Title, response.Director, response.Length);
    }

    public static PostBookRequest ToPostBookRequest(this Book book)
    {
        return new PostBookRequest { Title = book.Title, Author = book.Author, Pages = book.Pages };
    }

    public static PostMovieRequest ToPostMovieRequest(this Movie movie)
    {
        return new PostMovieRequest { Title = movie.Title, Director = movie.Director, Length = movie.Length };
    }
}
