using Microsoft.AspNetCore.Mvc;
using reco_system.Services;

namespace reco_system.Controllers
{
    public class MoviesController : Controller
    {
        private readonly TmdbService _tmdbService;

        public MoviesController(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        public async Task<IActionResult> TmdbDetails(int id)
        {
            var movie = await _tmdbService.GetMovieDetails(id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }
    }
}