using Ardalis.GuardClauses;
using Grpc.Core;
using MediaRegister.gRPC.Data;
using MediaRegister.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaRegister.gRPC.Services;

public class MediaService : Media.MediaBase
{
    private readonly ILogger<MediaService> _logger;
    private readonly AppDbContext _dbContext;

    public MediaService(ILogger<MediaService> logger, AppDbContext context)
    {
        _logger = logger;
        _dbContext = context;
    }

    #region Books
    public override async Task<SaveBookResponse> SaveBook(SaveBookRequest request, ServerCallContext context)
    {
        try
        {
            Guard.Against.NegativeOrZero(request.Pages, nameof(request.Pages));
            Guard.Against.NullOrWhiteSpace(request.Title, nameof(request.Title));
            Guard.Against.NullOrWhiteSpace(request.Author, nameof(request.Author));
        }
        catch (ArgumentException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }

        Book book = new Book(request.Title, request.Author, request.Pages);
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
        return new SaveBookResponse { Id = book.Id };
    }

    public override async Task<GetBookResponse> GetBook(GetBookRequest request, ServerCallContext context)
    {
        try
        {
            Guard.Against.NegativeOrZero(request.Id, nameof(request.Id));
        }
        catch (ArgumentException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }

        var book = await _dbContext.Books.FirstOrDefaultAsync(b => b.Id == request.Id);

        return book is null
            ? throw new RpcException(new Status(StatusCode.NotFound, $"Book with id {request.Id} not found"))
            : new GetBookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Pages = book.Pages
            };
    }


    public override async Task<GetBooksResponse> GetBooks(GetBooksRequest request, ServerCallContext context)
    {
        var books = await _dbContext.Books.ToListAsync();

        var response = new GetBooksResponse();

        response.Books.AddRange(books.Select(b => new GetBookResponse
        {
            Id = b.Id,
            Title = b.Title,
            Author = b.Author,
            Pages = b.Pages
        }));

        return response;
    }
    #endregion

    #region Movies
    public override Task<GetMovieResponse> GetMovie(GetMovieRequest request, ServerCallContext context)
    {
        try
        {
            Guard.Against.NegativeOrZero(request.Id, nameof(request.Id));
        }
        catch (ArgumentException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }

        var movie = _dbContext.Movies.FirstOrDefault(m => m.Id == request.Id);

        return movie is not null
            ? Task.FromResult(new GetMovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Length = movie.Length
            })
            : throw new RpcException(new Status(StatusCode.NotFound, $"Movie with id {request.Id} not found"));
    }

    public override async Task<GetMoviesResponse> GetMovies(GetMoviesRequest request, ServerCallContext context)
    {
        var movies = await _dbContext.Movies.ToListAsync();

        var response = new GetMoviesResponse();

        response.Movies.AddRange(movies.Select(m => new GetMovieResponse
        {
            Id = m.Id,
            Title = m.Title,
            Director = m.Director,
            Length = m.Length
        }));

        return response;
    }

    public override async Task<SaveMovieResponse> SaveMovie(SaveMovieRequest request, ServerCallContext context)
    {
        try
        {
            Guard.Against.NegativeOrZero(request.Length, nameof(request.Length));
            Guard.Against.NullOrWhiteSpace(request.Title, nameof(request.Title));
            Guard.Against.NullOrWhiteSpace(request.Director, nameof(request.Director));
        }
        catch (ArgumentException ex)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ex.Message));
        }

        var movie = new Movie(0, request.Title, request.Director, request.Length);
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();
        return new SaveMovieResponse { Id = movie.Id };
    }

    #endregion
}
