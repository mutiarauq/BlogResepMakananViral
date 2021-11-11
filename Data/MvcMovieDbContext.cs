using Microsoft.EntityFrameworkCore;
using MvcMovie.Models; 
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MvcMovie.Data

{
public class MvcMovieDbContext : IdentityDbContext<Pengguna>
{
public MvcMovieDbContext (DbContextOptions<MvcMovieDbContext> options) : base(options)
{ }
public DbSet<Resep> produk { get; set; }



}
}
