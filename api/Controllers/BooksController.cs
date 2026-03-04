using api.Models;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    // Lista temporária de livros (mais tarde virá do banco)
    private static readonly List<Book> Books = new()
    {
        new Book
        {
            Id = 1,
            Title = "1984",
            Author = "George Orwell",
            Genre = "Dystopian",
            YearPublished = 1949,
            Description = "A dystopian novel about totalitarian regime."
        },
        new Book
        {
            Id = 2,
            Title = "The Hobbit",
            Author = "J.R.R. Tolkien",
            Genre = "Fantasy",
            YearPublished = 1937,
            Description = "A hobbit goes on an unexpected adventure."
        },
        new Book
        {
            Id = 3,
            Title = "To Kill a Mockingbird",
            Author = "Harper Lee",
            Genre = "Fiction",
            YearPublished = 1960,
            Description = "A story about racial injustice in the Deep South."
        }
    };

    // GET /books
    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAll()
    {
        return Ok(Books);
    }

    // GET /books/{id}
    [HttpGet("{id}")]
    public ActionResult<Book> GetById(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);

        if (book == null)
            return NotFound();

        return Ok(book);
    }
}