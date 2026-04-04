using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reco_system.Data;

namespace reco_system.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class ApiBooksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public ApiBooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _db.Books.ToListAsync();
            return Ok(books);
        }
    }
}