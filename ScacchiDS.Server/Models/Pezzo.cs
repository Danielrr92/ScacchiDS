﻿using System.ComponentModel.DataAnnotations;
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
    }
}