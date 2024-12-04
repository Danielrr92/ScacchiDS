using Microsoft.EntityFrameworkCore;
using ScacchiDS.Server.Data;
using ScacchiDS.Server.DTOs;
using ScacchiDS.Server.Models;
using System.Net.WebSockets;

namespace ScacchiDS.Server.Services
{
    public class GameService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GameService> _logger;

        private static readonly Random _random = new Random();

        public GameService(ApplicationDbContext context, ILogger<GameService> logger)
        {
            _context = context;
            _logger = logger;
        }


        public async Task<GameDto> CreateNewGameAsync(string gameSessionId, string sessionIdPlayer1, string sessionIdPlayer2)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Scacchiera (non è un oggetto del DB)
                Scacchiera scacchiera = new Scacchiera();

                // Assegnazione casuale dei colori
                bool isPlayer1White = _random.Next(0, 2) == 0;

                // Creazione dei giocatori
                Player player1 = new Player { sessionId = sessionIdPlayer1, ColoreId = isPlayer1White ? Colore.BIANCO : Colore.NERO };
                Player player2 = new Player { sessionId = sessionIdPlayer2, ColoreId = isPlayer1White ? Colore.NERO : Colore.BIANCO };

                

                // Creazione della partita
                Partita partita = new Partita(player1, player2, gameSessionId);

                _context.Add(player1);
                _context.Add(player2);
                _context.Add(partita);
                await _context.SaveChangesAsync();

                // Commit della transazione
                await transaction.CommitAsync();

                // Creazione del DTO
                GameDto gameDto = new GameDto
                {
                    GameSessionId = gameSessionId,
                    Player1SessionId = sessionIdPlayer1,
                    Player2SessionId = sessionIdPlayer2,
                    Player1Color = player1.ColoreId,
                    Player2Color = player2.ColoreId,
                    ChessBoard = scacchiera.ToStringArray()
                };

                return gameDto;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($"Errore durante la creazione della partita: {ex.Message}");
                throw;
            }
        }

    }
}
