using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MvcMovie.Data

{
public class KatalogDbContext : IdentityDbContext<Pengguna>
{
public KatalogDbContext (DbContextOptions<KatalogDbContext> options) : base(options)
{ }
public DbSet<PenambahanMakanan> PenambahanMakanan1 { get; set; }
public DbSet<Produk> Produk { get; set; }
public DbSet<Movie> Movies { get; set; }

}
}
