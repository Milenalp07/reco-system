using Microsoft.AspNetCore.Mvc;
using api.Services;

namespace api.Controllers;

[ApiController]
[Route("api/recommendations")]
public class RecommendationsController : ControllerBase
{
    private readonly RecommendationService _service;

    public RecommendationsController(RecommendationService service)
    {
        _service = service;
    }

    [HttpGet("movies")]
    public async Task<IActionResult> GetMovieRecommendations(
        [FromQuery] string? genre,
        [FromQuery] double? minRating)
    {
        var result = await _service.GetMovieRecommendations(genre, minRating);
        return Ok(result);
    }

    [HttpGet("books")]
    public async Task<IActionResult> GetBookRecommendations(
        [FromQuery] string? genre,
        [FromQuery] double? minRating)
    {
        var result = await _service.GetBookRecommendations(genre, minRating);
        return Ok(result);
    }
}