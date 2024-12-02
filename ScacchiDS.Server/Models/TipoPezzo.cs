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
    }
}
