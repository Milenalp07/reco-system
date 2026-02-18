using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class RecommendationsController : ControllerBase
{
    // Temporary data source (same idea as MoviesController)
    private static readonly List<Movie> Movies = new()
    {
        new Movie { Id = 1, Title = "Inception", Genre = "Sci-Fi", ReleaseYear = 2010, Rating = 4.8 },
        new Movie { Id = 2, Title = "The Dark Knight", Genre = "Action", ReleaseYear = 2008, Rating = 4.9 },
        new Movie { Id = 3, Title = "Interstellar", Genre = "Sci-Fi", ReleaseYear = 2014, Rating = 4.7 },
        new Movie { Id = 4, Title = "Mad Max: Fury Road", Genre = "Action", ReleaseYear = 2015, Rating = 4.6 }
    };

    // GET /recommendations?genre=Sci-Fi
    [HttpGet]
    public ActionResult<IEnumerable<Movie>> Get([FromQuery] string genre)
    {
        var recommendations = Movies
            .Where(m => m.Genre.Equals(genre, StringComparison.OrdinalIgnoreCase))
            .OrderByDescending(m => m.Rating)
            .Take(3)
            .ToList();

        return Ok(recommendations);
    }
}
