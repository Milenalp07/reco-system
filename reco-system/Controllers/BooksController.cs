using Microsoft.AspNetCore.Mvc;
using reco_system.Data;

namespace reco_system.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult Index()
        {
            var books = FakeData.Books;
            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = FakeData.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
                return NotFound();

            return View(book);
        }
    }
}