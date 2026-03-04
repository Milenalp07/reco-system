using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _db;

    public BooksController(AppDbContext db)
    {
        _db = db;
    }

    // GET /books
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetAll()
    {
        var books = await _db.Books.AsNoTracking().ToListAsync();
        return Ok(books);
    }

    // GET /books/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetById(int id)
    {
        var book = await _db.Books.AsNoTracking()
                                  .FirstOrDefaultAsync(b => b.Id == id);

        if (book is null)
            return NotFound();

        return Ok(book);
    }
}