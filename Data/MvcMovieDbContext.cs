using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
namespace MvcMovie.Data

{
public class MvcMovieDbContext : DbContext
{
public MvcMovieDbContext (DbContextOptions<MvcMovieDbContext> options) : base(options)
{ }
public DbSet<Movie> Movies { get; set; }
}
}