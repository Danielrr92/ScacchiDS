using ScacchiDS.Server.Data;
using ScacchiDS.Server.Models;

namespace ScacchiDS.Server.Services
{
    public class PartitaService
    {
        private readonly ApplicationDbContext _context;

        public PartitaService(ApplicationDbContext context)
        {
            _context = context;
        }


        internal async Task CreaPartita(Giocatore giocatore1, Giocatore giocatore2) 
        {
            Partita partita = new Partita();
            partita.GiocatoreBianco = giocatore1;
            partita.GiocatoreNero = giocatore2;
            partita.EsitoPartitaId = EsitoPartita.ID_ESITO_IN_CORSO;
        }
    }
}
