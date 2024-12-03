using ScacchiDS.Server.Data;
using ScacchiDS.Server.Models;

namespace ScacchiDS.Server.Services
{
    public class GameService
    {
        private readonly ApplicationDbContext _context;

        public GameService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Game> CreateNewGameAsync(string player1Id, string player2Id)
        {
            var newGame = new Game
            {
                Player1Id = player1Id,
                Player2Id = player2Id,
                State = "Waiting",
                CreatedAt = DateTime.UtcNow
            };

            _context.Games.Add(newGame);
            await _context.SaveChangesAsync();
            return newGame;
        }
    }
}
