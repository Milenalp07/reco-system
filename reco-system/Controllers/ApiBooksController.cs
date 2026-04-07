using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reco_system.Data;

namespace reco_system.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksApiController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BooksApiController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _db.Books
                .AsNoTracking()
                .Select(book => new
                {
                    id = book.Id,
                    title = book.Title,
                    author = book.Author,
                    genre = book.Genre,
                    yearPublished = book.YearPublished,
                    description = book.Description,
                    rating = book.Rating,
                    isbn = book.Isbn,
                    imageUrl = !string.IsNullOrWhiteSpace(book.ImageUrl)
                        ? book.ImageUrl
                        : (!string.IsNullOrWhiteSpace(book.Isbn)
                            ? $"https://covers.openlibrary.org/b/isbn/{book.Isbn.Replace("-", "").Trim()}-L.jpg"
                            : "https://via.placeholder.com/300x450?text=No+Image")
                })
                .ToListAsync();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _db.Books
                .AsNoTracking()
                .Where(b => b.Id == id)
                .Select(book => new
                {
                    id = book.Id,
                    title = book.Title,
                    author = book.Author,
                    genre = book.Genre,
                    yearPublished = book.YearPublished,
                    description = book.Description,
                    rating = book.Rating,
                    isbn = book.Isbn,
                    imageUrl = !string.IsNullOrWhiteSpace(book.ImageUrl)
                        ? book.ImageUrl
                        : (!string.IsNullOrWhiteSpace(book.Isbn)
                            ? $"https://covers.openlibrary.org/b/isbn/{book.Isbn.Replace("-", "").Trim()}-L.jpg"
                            : "https://via.placeholder.com/300x450?text=No+Image")
                })
                .FirstOrDefaultAsync();

            if (book == null)
                return NotFound();

            return Ok(book);
        }
    }
}