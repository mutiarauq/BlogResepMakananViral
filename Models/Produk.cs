using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Produk
    {
        public int Id {get; set;}
        public string NamaMakanan {get; set;}
        public string kategori {get; set;}
        public string Bahan {get; set;}
        public string Alat {get; set;}
        public string CaraPembuatan {get; set;}
        
    }
}