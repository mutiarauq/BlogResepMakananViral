using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MvcMovie.Data;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Controllers
{

public class ResepController : Controller
    {
        private MvcMovieDbContext _context;
        public  ResepController(MvcMovieDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString, string resepKategori)
        {
            var kategoris = _context.produk.Select(x => x.kategori).Distinct();
            var Produk = _context.produk.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                Produk = Produk.Where(x => x.NamaMakanan.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(resepKategori))
            {
                Produk = Produk.Where(x => x.kategori == resepKategori);
            }
            var viewModel = new ResepKategoriViewModel();
           

            return View(await Produk.ToListAsync());
        }
        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int id, [Bind("Id, NamaMakanan, kategori, Bahan, Alat, CaraPembuatan")] Resep resep)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                _context.produk.Update(resep);
                _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if ( !MovieExist(resep.Id))
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
                return View(resep);
        }

        public IActionResult Edit(int ? id)
        {
            if (id == null)
            return NotFound();
            var resep = _context.produk.Find(id);
            if (resep == null)
            return NotFound();
            return View(resep);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int ? id, [Bind("Id, NamaMakanan, kategori, Bahan, Alat, CaraPembuatan")] Resep resep)
        {
            if (resep.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                _context.produk.Update(resep);
                _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if ( !MovieExist(resep.Id))
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
                return View(resep);
        }

        private bool MovieExist(int id)
        {
            var resep = _context.produk.Find(id);
            return resep != null;
        }
       

        public IActionResult Details(int id)
        {
            var resep = _context.produk.Find(id);
            return View(resep);
        }

        public IActionResult Delete(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var resep= _context.produk.Find(id);
            if(resep == null)
            {
                return NotFound();
            }
            return View(resep);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeleteConfirmed(int ? id)
        {
            var resep= _context.produk.Find(id);
            _context.produk.Remove(resep);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            
            
        }
        
    }
}
