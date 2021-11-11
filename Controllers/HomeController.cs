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

namespace MvcMovie.Controllers
{
    [Authorize]
     public class HomeController : Controller
    {
       
        private UserManager<Pengguna> _userManager;
        private ILogger<HomeController> _logger;
        MvcMovieDbContext _context;

        public HomeController(ILogger<HomeController> logger, MvcMovieDbContext context, UserManager<Pengguna> userManager)
        {
            
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

    

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tabel()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            
            
        }
        
    }
}
