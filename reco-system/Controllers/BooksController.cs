using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using reco_system.Data;
using reco_system.Models;

namespace reco_system.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _db.Books.ToListAsync();
            ApplyBookImages(books);
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            ApplyBookImage(book);

            var related = await _db.Books
                .Where(b => b.Id != id &&
                    (
                        b.Genre == book.Genre ||
                        b.Author == book.Author ||
                        Math.Abs(b.Rating - book.Rating) <= 0.5
                    ))
                .OrderByDescending(b => b.Rating)
                .Take(6)
                .ToListAsync();

            ApplyBookImages(related);

            ViewBag.Related = related;
            return View(book);
        }

        [Authorize(Roles = "Admin,User")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Add(Book book)
        {
            if (!string.IsNullOrWhiteSpace(book.Isbn) && string.IsNullOrWhiteSpace(book.ImageUrl))
            {
                book.ImageUrl = BuildOpenLibraryCoverUrl(book.Isbn);
            }

            if (!ModelState.IsValid)
                return View(book);

            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            ApplyBookImage(book);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!string.IsNullOrWhiteSpace(book.Isbn) && string.IsNullOrWhiteSpace(book.ImageUrl))
            {
                book.ImageUrl = BuildOpenLibraryCoverUrl(book.Isbn);
            }

            if (!ModelState.IsValid)
                return View(book);

            _db.Books.Update(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            ApplyBookImage(book);
            return View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if (book == null) return NotFound();

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private static void ApplyBookImages(IEnumerable<Book> books)
        {
            foreach (var book in books)
            {
                ApplyBookImage(book);
            }
        }

        private static void ApplyBookImage(Book book)
        {
            if (book == null) return;

            if (string.IsNullOrWhiteSpace(book.ImageUrl) && !string.IsNullOrWhiteSpace(book.Isbn))
            {
                book.ImageUrl = BuildOpenLibraryCoverUrl(book.Isbn);
            }

            if (string.IsNullOrWhiteSpace(book.ImageUrl))
            {
                book.ImageUrl = "https://via.placeholder.com/300x450?text=No+Image";
            }
        }

        private static string BuildOpenLibraryCoverUrl(string isbn)
        {
            var cleanIsbn = isbn.Replace("-", "").Trim();
            return $"https://covers.openlibrary.org/b/isbn/{cleanIsbn}-L.jpg";
        }
    }
}