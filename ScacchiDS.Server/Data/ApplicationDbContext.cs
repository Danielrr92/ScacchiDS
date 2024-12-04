
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ScacchiDS.Server.Models;
using System.Reflection.Emit;

namespace ScacchiDS.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Player> Giocatori { get; set; }
        public DbSet<Partita> Partite { get; set; }
        public DbSet<Mossa> Mosse { get; set; }
        public DbSet<EsitoPartita> EsitiPartita { get; set; }
        public DbSet<TipoPezzo> TipiPezzo { get; set; }
        public DbSet<Colore> Colori {  get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configurazione delle relazioni (se necessaria)
            builder.Entity<Partita>()
                .HasOne(p => p.Player1)
                .WithMany()
                .HasForeignKey(p => p.Player1Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Partita>()
                .HasOne(p => p.Player2)
                .WithMany()
                .HasForeignKey(p => p.Player2Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Mossa>()
                .HasOne(m => m.Partita)
                .WithMany()
                .HasForeignKey(m => m.PartitaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Mossa>()
                .HasOne(m => m.Player)
                .WithMany()
                .HasForeignKey(m => m.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Esegui il seeding per la tabella Esito
            builder.Entity<EsitoPartita>().HasData(
                new EsitoPartita { Id = 1, Descrizione = "Vittoria Bianco" },
                new EsitoPartita { Id = 2, Descrizione = "Vittoria Nero" },
                new EsitoPartita { Id = 3, Descrizione = "Patta" },
                new EsitoPartita { Id = 4, Descrizione = "In Corso" }
            );

            builder.Entity<TipoPezzo>().HasData(
                new TipoPezzo { Id = 1, Descrizione = "Re" },
                new TipoPezzo { Id = 2, Descrizione = "Regina" },
                new TipoPezzo { Id = 3, Descrizione = "Torre" },
                new TipoPezzo { Id = 4, Descrizione = "Alfiere" },
                new TipoPezzo { Id = 5, Descrizione = "Cavallo" },
                new TipoPezzo { Id = 6, Descrizione = "Pedone" }
            );

            builder.Entity<Colore>().HasData(
                new Colore { Id = 1, Descrizione = "Bianco" },
                new Colore { Id = 2, Descrizione = "Nero" }
            );

            

        }


    }
}
