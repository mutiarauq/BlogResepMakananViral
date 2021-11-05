using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Models
{
    public class Kategori : IdentityUser
    {
        public string ID { get; set; }
        public string Cemilan { get; set; }
        public string Makanan { get; set; }
        public string Minuman { get; set; }

        
    }
}