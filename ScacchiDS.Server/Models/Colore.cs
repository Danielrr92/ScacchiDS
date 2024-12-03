using System.ComponentModel.DataAnnotations;

namespace ScacchiDS.Server.Models
{
    public class Colore
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(6)]
        public string Descrizione { get; set; }  // Bianco - Nero

        public const int BIANCO = 1;
        public const int NERO = 2;

    }
}
