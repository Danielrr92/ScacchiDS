using Microsoft.AspNetCore.Mvc;
using ScacchiDS.Server.Models;
using ScacchiDS.Server.Services;

namespace ScacchiDS.Server.Controllers
{
    public class GameController : Controller
    {
        private readonly GameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(GameService gameServce, ILogger<GameController> logger)
        {
            _gameService = gameServce;
            _logger = logger;
        }


        
    }
}
