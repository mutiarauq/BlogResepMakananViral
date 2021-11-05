using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace MvcMovie.Models
{
    public class PenambahanMakanan : IdentityUser
    {
        public string MakananID { get; set; }
        public string TransaksiID { get; set; }
        
        
    }
}