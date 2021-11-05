using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Models
{
    public class Pengguna : IdentityUser
    {
        
        public List<Pengguna> Models { get; set; }
    }
}