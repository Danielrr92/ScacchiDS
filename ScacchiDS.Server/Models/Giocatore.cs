using System.ComponentModel.DataAnnotations;

namespace ScacchiDS.Server.Models
{
    public class Giocatore
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AspNetUserId { get; set; }  
        public virtual ApplicationUser AspNetUser { get; set; }  // Navigational property

        [Required]
        public string Username { get; set; }

        // Data di registrazione del giocatore, se necessario
        public DateTime DataRegistrazione { get; set; }

        // Altre proprietà che potrebbero essere rilevanti
    }
}
