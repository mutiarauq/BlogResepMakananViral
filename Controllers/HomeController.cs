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
using MvcMovie.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MvcMovie.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcMovie.Controllers
{
    [Authorize]
     public class HomeController : Controller
    {
       
        
        

    private MvcMovieDbContext _context;
        public  HomeController(MvcMovieDbContext context)
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
        public IActionResult Tabel()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var resep = _context.produk.Find(id);
            return View(resep);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            
            
        }
        
    }
}
