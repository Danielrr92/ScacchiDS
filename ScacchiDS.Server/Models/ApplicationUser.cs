using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ScacchiDS.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Player? Giocatore { get; set; }  // Navigational property (optional)
    }
}
