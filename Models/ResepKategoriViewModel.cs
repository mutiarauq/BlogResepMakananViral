using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace MvcMovie.Models
{
    public class ResepKategoriViewModel
    {
        public List<Produk> produk {get; set;}
        public SelectList kategori {get; set;}
        public string searchString {get; set;}
        public string resepKategori {get; set;}
    }
}