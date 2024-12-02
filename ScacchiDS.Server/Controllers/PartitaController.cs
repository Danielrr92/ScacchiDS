using Microsoft.AspNetCore.Mvc;
using ScacchiDS.Server.Models;
using ScacchiDS.Server.Services;

namespace ScacchiDS.Server.Controllers
{
    public class PartitaController : Controller
    {
        private readonly PartitaService _partitaService;

        public PartitaController(PartitaService partitaService)
        {
            _partitaService = partitaService;
        }


        [HttpPost("CreaPartita")]
        public async Task<IActionResult> CreaPartita([FromBody] Giocatore giocatore1, [FromBody] Giocatore giocatore2)
        {
            //Partita partita = await _partitaService.CreaPartita(giocatore1, giocatore2);
            return Ok(); //Ok(partita);
        }
    }
}
