using Microsoft.AspNetCore.Mvc;
using reco_system.Services;

namespace reco_system.Controllers
{
    [ApiController]
    [Route("api/googlebooks")]
    public class GoogleBooksController : ControllerBase
    {
        private readonly GoogleBooksService _googleBooksService;

        public GoogleBooksController(GoogleBooksService googleBooksService)
        {
            _googleBooksService = googleBooksService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            var books = await _googleBooksService.SearchBooksAsync(query);
            return Ok(books);
        }
    }
}