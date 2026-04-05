using Microsoft.AspNetCore.Mvc;
using reco_system.Services;

namespace reco_system.Controllers
{
    public class HomeController : Controller
    {
        private readonly TmdbService _tmdbService;

        public HomeController(TmdbService tmdbService)
        {
            _tmdbService = tmdbService;
        }

        public async Task<IActionResult> Index()
        {
            var featured = await _tmdbService.GetPopularMovies();
            ViewBag.Featured = featured;
            return View();
        }
    }
}