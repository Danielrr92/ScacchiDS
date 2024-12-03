using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScacchiDS.Server.Models
{
    public class Pezzo
    {
        [Key]
        public int Id { get; set; }  // ID del pezzo


        [ForeignKey("TipoPezzo")]
        public int TipoPezzoId { get; set; }
        public virtual TipoPezzo TipoPezzo { get; set; }

        [ForeignKey("Colore")]
        public int ColoreId { get; set; }
        public virtual Colore Colore { get; set; }


        [MaxLength(1)]
        public string Simbolo { get; set; }  // Es. "P" per Pedone, "T" per Torre, ecc.

        public const string SIMBOLO_RE = "R";
        public const string SIMBOLO_DONNA = "D"; 
        public const string SIMBOLO_TORRE = "T";
        public const string SIMBOLO_ALFIERE = "A";
        public const string SIMBOLO_CAVALLO = "C";
        public const string SIMBOLO_PEDONE = "";
    }
}
