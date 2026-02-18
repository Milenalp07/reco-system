using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    // Temporary in-memory list (later this will come from the database)
    private static readonly List<Movie> Movies = new()
    {
        new Movie
        {
            Id = 1,
            Title = "Inception",
            Genre = "Sci-Fi",
            ReleaseYear = 2010,
            Description = "A thief who steals corporate secrets through dream-sharing technology.",
            Rating = 4.8
        },
        new Movie
        {
            Id = 2,
            Title = "The Dark Knight",
            Genre = "Action",
            ReleaseYear = 2008,
            Description = "Batman faces the Joker in Gotham City.",
            Rating = 4.9
        },
        new Movie
        {
            Id = 3,
            Title = "Interstellar",
            Genre = "Sci-Fi",
            ReleaseYear = 2014,
            Description = "A team travels through a wormhole in space to save humanity.",
            Rating = 4.7
        }
    };

    // GET /movies
    [HttpGet]
    public ActionResult<IEnumerable<Movie>> GetAll()
    {
        return Ok(Movies);
    }

    // GET /movies/{id}
    [HttpGet("{id}")]
    public ActionResult<Movie> GetById(int id)
    {
        var movie = Movies.FirstOrDefault(m => m.Id == id);

        if (movie == null)
            return NotFound();

        return Ok(movie);
    }
}
