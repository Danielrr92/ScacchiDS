using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ScacchiDS.Server.Models
{
    public class Partita
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataInizio { get; set; }

        public DateTime? DataFine { get; set; }

        [ForeignKey("GiocatoreBianco")]
        public int GiocatoreBiancoId { get; set; }
        public virtual Giocatore GiocatoreBianco { get; set; }

        [ForeignKey("GiocatoreNero")]
        public int GiocatoreNeroId { get; set; }
        public virtual Giocatore GiocatoreNero { get; set; }

        [ForeignKey("EsitoPartita")]
        public int? EsitoPartitaId { get; set; }
        public virtual EsitoPartita EsitoPartita { get; set; }

        public Partita()
        {
            DataInizio = DateTime.MinValue;
        }
    }
}
