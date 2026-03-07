using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Data;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetBooks()
    {
        var books = await _context.Books.ToListAsync();
        return Ok(books);
    }
}