using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using reco_system.Services;

namespace reco_system.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly TmdbService _tmdbService;

        public MoviesController(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Add() => View();

        public async Task<IActionResult> TmdbDetails(int id)
        {
            var movie = await _tmdbService.GetMovieDetails(id);
            if (movie == null) return NotFound();

            var similar = await _tmdbService.GetSimilarMovies(id);
            ViewBag.Similar = similar;

            return View(movie);
        }
    }
}