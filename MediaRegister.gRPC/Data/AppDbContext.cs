using MediaRegister.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace MediaRegister.gRPC.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Movie> Movies { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().ToTable("Books");
        modelBuilder.Entity<Movie>().ToTable("Movies");
    }
}
