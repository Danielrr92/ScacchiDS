using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ScacchiDS.Server.DTOs;

namespace ScacchiDS.Server.Models
{
    public class Partita
    {
        [Required]
        [Key]
        public int Id { get; set; }


        [Required]
        public string? SessionId { get; set; }


        [Required]
        public DateTime DataInizio { get; set; }


        public DateTime? DataFine { get; set; }


        [ForeignKey(nameof(Player1))]
        public int? Player1Id { get; set; }
        public virtual Player? Player1 { get; set; }


        [ForeignKey(nameof(Player2))]
        public int? Player2Id { get; set; }
        public virtual Player? Player2 { get; set; }


        [ForeignKey("EsitoPartita")]
        public int? EsitoPartitaId { get; set; }
        public virtual EsitoPartita? EsitoPartita { get; set; }


        public Partita()
        {

        }

        public Partita(Player player1, Player player2, string gameSessionId)
        {
            DataInizio = DateTime.Now;
            EsitoPartitaId = EsitoPartita.ID_ESITO_IN_CORSO;
            SessionId = gameSessionId;
            Player1 = player1;
            Player2 = player2;
        }

    }
}
