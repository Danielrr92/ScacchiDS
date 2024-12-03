using System.ComponentModel.DataAnnotations;

namespace ScacchiDS.Server.Models
{
    public class TipoPezzo
    {
        [Key]
        public int Id { get; set; }  

        [Required]
        [MaxLength(20)]
        public string Descrizione { get; set; }  // Es. "Pedone", "Torre", ecc.

        public const int RE = 1;
        public const int DONNA = 2;
        public const int TORRE = 3;
        public const int ALFIERE = 4;
        public const int CAVALLO = 5;
        public const int PEDONE = 6;

    }
}
