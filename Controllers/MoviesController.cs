using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using MvcMovie.Data;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;



namespace MvcMovie.Controllers
{
    public class  KatalogController : Controller
    {

        private KatalogDbContext _context;

        public  KatalogController( KatalogDbContext context)
        {
            _context = context;
        }
        
       
        public async Task<IActionResult> Index(string searchString, string movieGenre)
        {
            //SELECT DISTINCT MOVIES.Genre FROM Movies
            var Bahan = _context.Movies.Select(x => x.Bahan).Distinct();
            var movies = _context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(x => x.NamaMakanan.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.NamaMakanan == movieGenre);
            }

            var viewModel = new MovieGenreViewModel()
            {
                Movies = await movies.ToListAsync(),
                Genres = new SelectList(await Bahan.ToListAsync())
            };
            return View(viewModel);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,NamaMakanan,Kategori,Bahan,Alat,CaraPembuatan")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     _context.Movies.Add(movie);
                     await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var movie = _context.Movies.Find(id);
            if (movie == null)
                return NotFound();
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id, Title, ReleaseDate, Genre, Price, Rating")] Movie movie)
        {

            if(movie.Id != id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Movies.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if(!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(movie);
        }
        private bool MovieExists(int id)
        {
            var movie = _context.Movies.Find(id);
            return movie != null;
        }

        public IActionResult Show(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View (movie);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movie = _context.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View (movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var movie = _context.Movies.Find(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        

        
        
    }
}
