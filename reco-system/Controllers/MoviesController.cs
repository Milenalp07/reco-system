using Microsoft.AspNetCore.Mvc;
using reco_system.Data;

namespace reco_system.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            var movies = FakeData.Movies;
            return View(movies);
        }

        public IActionResult Details(int id)
        {
            var movie = FakeData.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }
    }
}