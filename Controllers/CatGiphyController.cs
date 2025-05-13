using Microsoft.AspNetCore.Mvc;
using PruebaDev.Models;
using PruebaDev.Services;
using System.Collections.Concurrent;
using System.Net.Http;
using System.Text.Json;

namespace PruebaDev.Controllers
{
    [ApiController]
    [Route("api")]
    public class CatGiphyController : ControllerBase
    {
        private readonly CatGiphyService _gifService;

        public CatGiphyController(CatGiphyService gifService)
        {
            _gifService = gifService;
        }

        [HttpGet("fact")]
        public async Task<IActionResult> GetFact()
        {
            var fact = await _gifService.GetCatFactAndGifAsync();

            return Ok(fact);
        }

        [HttpGet("gif")]
        public async Task<IActionResult> GetGif([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest(new { error = "Parámetro 'query' requerido" });

            var gifUrl = await _gifService.GetCatFactAndGifAsync();
            if (gifUrl == null)
                return NotFound(new { error = "No se encontró ningún GIF" });

            return Ok(new { gif = gifUrl });
        }

        [HttpGet("history")]
        public IActionResult GetHistory()
        {
            var historial = _gifService.GetHistorial();
            return Ok(historial);
        }
        [HttpPost("refresh-gif")]
        public async Task<IActionResult> RefreshGif([FromBody] string fact)
        {
            if (string.IsNullOrWhiteSpace(fact))
                return BadRequest("Frase inválida.");

            var palabras = fact.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var query = string.Join(" ", palabras.Take(3));

            var gifUrl = await _gifService.BuscarGifAsync(fact, query);
            if (gifUrl == null) return NotFound("No se encontró un gif.");

            return Ok(new { gifUrl });
        }
    }
}
