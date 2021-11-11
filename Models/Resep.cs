using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Resep
    {
        public int Id {get; set;}
        [StringLength(60, MinimumLength = 3)]
        public string NamaMakanan {get; set;}
        public string kategori {get; set;}
        public string Bahan {get; set;}
        public string Alat {get; set;}
        public string CaraPembuatan {get; set;}
    }
}