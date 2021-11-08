using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Models
{
    public class Pengguna : IdentityUser
    {
        public string Avatar { get; set; }
        public List<Pengguna> Models { get; set; }
    }
} 