using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScacchiDS.Server.Models
{
    public class Player
    {
        [Required]     
        [Key]
        public int Id { get; set; }


        [Required]
        public string? sessionId { get; set; }


        //se l'utente è anche registrato mi salvo l'id dell'utente legandolo al giocatore, se invece è un ospite questa proprietà rimane null
        [ForeignKey("AspNetUser")]
        public string? AspNetUserId { get; set; } 
        public virtual ApplicationUser? AspNetUser { get; set; }


        [Required]
        [ForeignKey("Colore")]
        public int ColoreId { get; set; }
        public virtual Colore? Colore { get; set; }


        [Required]
        public DateTime DataOraCreazione { get; set; }


        //costruttore
        public Player() 
        {
            DataOraCreazione = DateTime.Now;
        }


    }
}
