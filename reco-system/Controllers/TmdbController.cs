using Microsoft.AspNetCore.Mvc;
using reco_system.Services;

namespace reco_system.Controllers
{
    [ApiController]
    [Route("api/tmdb")]
    public class TmdbController : ControllerBase
    {
        private readonly TmdbService _tmdbService;

        public TmdbController(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var movies = await _tmdbService.SearchMovies(query);

            var result = movies
                .Where(m => !string.IsNullOrEmpty(m.Title))
                .Select(m => new
                {
                    id = m.Id,
                    title = m.Title,
                    rating = m.Vote_Average,
                    imageUrl = string.IsNullOrEmpty(m.Poster_Path)
                        ? null
                        : $"https://image.tmdb.org/t/p/w500{m.Poster_Path}"
                });

            return Ok(result);
        }
    }
}