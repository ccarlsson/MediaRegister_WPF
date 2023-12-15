using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using MediaRegister_WPF.Extensions;
using MediaRegister_WPF.Models;

namespace MediaRegister_WPF.Services;
internal class MediaService
{

    Media.MediaClient _client;
    string _url = "http://172.16.30.199:9080";

    public MediaService()
    {
        var channel = GrpcChannel.ForAddress(_url);
        _client = new Media.MediaClient(channel);
    }


    public async Task<Book> GetBook(int id)
    {
        var request = new GetBookRequest { Id = id };
        var response = await _client.GetBookAsync(request);

        return response.ToBook();
    }

    public async Task<Movie> GetMovie(int id)
    {
        var request = new GetMovieRequest { Id = id };
        var response = await _client.GetMovieAsync(request);

        return response.ToMovie();
    }

    public async Task<List<Models.Media>> GetAllMedia()
    {
        var media = new List<Models.Media>();
        var bookRequest = new GetBooksRequest();
        var bookResponse = _client.GetBooks(bookRequest);
        var movieRequest = new GetMoviesRequest();
        var movieResponse = _client.GetMovies(movieRequest);

        await foreach (var book in bookResponse.ResponseStream.ReadAllAsync())
        {
            media.Add(book.ToBook());
        }

        await foreach (var movie in movieResponse.ResponseStream.ReadAllAsync())
        {
            media.Add(movie.ToMovie());
        }

        return media; 
    }

    public async Task<int> AddBook(Book book)
    {
        var request = book.ToPostBookRequest();
        var response = await _client.PostBookAsync(request);

        return response.Id;
    }
    
    public async Task<int> AddMovie(Movie movie)
    {
        var request = movie.ToPostMovieRequest();
        var response = await _client.PostMovieAsync(request);

        return response.Id;
    }
}
