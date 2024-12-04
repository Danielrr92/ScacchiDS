using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScacchiDS.Server.Models
{
    public class Giocatore
    {
        [Key]
        public string? sessionId { get; set; }


        [ForeignKey("AspNetUser")]
        public string? AspNetUserId { get; set; } 
        public virtual ApplicationUser? AspNetUser { get; set; }    

        
        public DateTime DataOraCreazione { get; set; }

        
    }
}
