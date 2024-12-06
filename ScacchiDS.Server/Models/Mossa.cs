using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ScacchiDS.Server.Models
{
    public class Mossa
    {
        [Key]
        public int Id { get; set; }


        [ForeignKey("Partita")]
        public int PartitaId { get; set; }
        public virtual required Partita Partita { get; set; }


        [Required]
        public int NumeroMossa { get; set; }


        [ForeignKey("Player")]
        public int PlayerId { get; set; }
        public virtual required Player Player { get; set; }


        [Required]
        public int PezzoId { get; set; }  // ID del pezzo
        public virtual required Pezzo Pezzo { get; set; }  // Relazione con la classe Pezzo


        [Required]
        [MaxLength(5)]
        public required string PosizioneIniziale { get; set; }  // Es. "e2"DS


        [Required]
        [MaxLength(5)]
        public required string PosizioneFinale { get; set; }  // Es. "e4"


        //public Mossa() 
        //{

        //}


        //public Mossa(Partita partita, Giocatore giocatore, Pezzo pezzo, string posizioneIniziale, string posizioneFinale )
        //{
        //    Partita = partita;
        //    Giocatore = giocatore;
        //    Pezzo = pezzo;
        //    PosizioneIniziale = posizioneIniziale;
        //    PosizioneFinale = posizioneFinale;
        //}
    }
}
