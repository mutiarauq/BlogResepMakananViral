using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string NamaMakanan { get; set; }

        [Display(Name = "Kategori")]
        [StringLength(200)]
        [Required]
        public string Kategori { get; set; }

        [StringLength(60)]
        [Required]
        public string Bahan { get; set; }
        
        [StringLength(500)]
        [Required]
        
        public string Alat { get; set; }

        [StringLength(500)]
        [Required]
        public string CaraPembuatan { get; set; }

        
    } 
    
}
