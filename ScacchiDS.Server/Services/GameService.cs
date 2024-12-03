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
            



        }
    }
}
