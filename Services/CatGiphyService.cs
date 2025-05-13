using Microsoft.EntityFrameworkCore;
using PruebaDev.BaseDatos;
using PruebaDev.Controllers;
using PruebaDev.Models;
using System;
using System.Collections.Concurrent;
using System.Text.Json;

namespace PruebaDev.Services
{
    public class CatGiphyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _giphyApiKey = "voaNIOg1u7ONPbckzWK71C48YqCOkhVP";
        private static readonly ConcurrentBag<GifSearch> _historial = new();
        private readonly ContextBD _context;

        public CatGiphyService(HttpClient httpClient, ContextBD context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<CatGifResult?> GetCatFactAndGifAsync()
        {
       
            var catfactResponse = await _httpClient.GetAsync("https://catfact.ninja/fact");
            if (!catfactResponse.IsSuccessStatusCode) return null;

            using var stream = await catfactResponse.Content.ReadAsStreamAsync();
            using var json = await JsonDocument.ParseAsync(stream);
            var fact = json.RootElement.GetProperty("fact").GetString();
            if (string.IsNullOrWhiteSpace(fact)) return null;

   
            var palabras = fact.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var query = string.Join(" ", palabras.Take(3));

            Console.WriteLine($"Fact obtenido: {fact}");
            var gifUrl = await BuscarGifAsync(fact, query);
            if (gifUrl == null) return null;

            return new CatGifResult
            {
                Fact = fact,
                GifUrl = gifUrl
            };
        }

        public async Task<string?> BuscarGifAsync(string fact, string query)
        {
            var url = $"https://api.giphy.com/v1/gifs/search?api_key={_giphyApiKey}&q={Uri.EscapeDataString(query)}&limit=10&rating=g&lang=en";
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode) return null;

            using var stream = await response.Content.ReadAsStreamAsync();
            using var json = await JsonDocument.ParseAsync(stream);
            var gifs = json.RootElement.GetProperty("data");
            if (gifs.GetArrayLength() == 0) return null;

            var random = new Random();
            var gif = gifs[random.Next(gifs.GetArrayLength())];
            var gifUrl = gif.GetProperty("images").GetProperty("original").GetProperty("url").GetString();

            var entrada = new GifSearch
            {
                FraseCompleta = fact,
                QueryFact = query,
                GifUrl = gifUrl!,
                FechaBusqueda = DateTime.UtcNow
            };

            _context.Historial.Add(entrada);
            await _context.SaveChangesAsync();

            return gifUrl;
        }

        public IEnumerable<GifSearch> GetHistorial()
        {
            return _context.Historial
            .AsNoTracking()
            .OrderByDescending(h => h.FechaBusqueda)
            .ToList();
        }
    }
}
