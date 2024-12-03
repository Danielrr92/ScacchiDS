using ScacchiDS.Server.Data;
using ScacchiDS.Server.Models;
using System.Net.WebSockets;

namespace ScacchiDS.Server.Services
{
    public class GameService
    {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task CreateNewGameAsync(string gameSessionId, string sessionIdPlayer1, string sessionIdPlayer2)
        {
            //scacchiera
            Scacchiera scacchiera = new Scacchiera();

            //giocatori
            Giocatore player1 = new Giocatore { sessionId = sessionIdPlayer1 };            
            Giocatore player2 = new Giocatore { sessionId = sessionIdPlayer2 };

            // Crea un'istanza di Random per la scelta casuale
            Random random = new Random();
            bool isPlayer1White = random.Next(0, 2) == 0; // 0 o 1

            //partita
            Partita partita = new Partita();
            partita.GiocatoreBianco = isPlayer1White ? player1 : player2;
            partita.GiocatoreNero = isPlayer1White ? player2 : player1;




        }
    }
}
