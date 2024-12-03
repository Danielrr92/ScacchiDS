using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScacchiDS.Server.Models
{
    public class Giocatore
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AspNetUser")]
        public string? AspNetUserId { get; set; } // Chiave esterna opzionale

        public virtual ApplicationUser? AspNetUser { get; set; } // Proprietà di navigazione

        [Required]
        public string Username { get; set; }

        // Data di registrazione del giocatore, se necessario
        public DateTime DataRegistrazione { get; set; }

        // Altre proprietà che potrebbero essere rilevanti
    }
}
