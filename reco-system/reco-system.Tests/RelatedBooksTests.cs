using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using reco_system.Controllers;
using reco_system.Data;
using reco_system.Models;

namespace reco_system.Tests
{
    public class RelatedBooksTests
    {
        private ApplicationDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Details_ShouldReturnRelatedBooksBySameGenre()
        {
            var db = GetInMemoryDb();
            db.Books.AddRange(
                new Book { Id = 1, Title = "Book A", Author = "Author", Genre = "Fantasy", Rating = 4.5 },
                new Book { Id = 2, Title = "Book B", Author = "Author", Genre = "Fantasy", Rating = 4.0 },
                new Book { Id = 3, Title = "Book C", Author = "Author", Genre = "Horror", Rating = 3.0 }
            );
            await db.SaveChangesAsync();

            var controller = new BooksController(db);
            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var related = viewResult.ViewData["Related"] as IEnumerable<Book>;

            Assert.NotNull(related);
            Assert.Contains(related, b => b.Genre == "Fantasy");
            Assert.DoesNotContain(related, b => b.Id == 1);
        }

        [Fact]
        public async Task Details_RelatedBooks_ShouldNotIncludeCurrentBook()
        {
            var db = GetInMemoryDb();
            db.Books.AddRange(
                new Book { Id = 1, Title = "Book A", Author = "Author", Genre = "Fantasy", Rating = 4.5 },
                new Book { Id = 2, Title = "Book B", Author = "Author", Genre = "Fantasy", Rating = 4.0 }
            );
            await db.SaveChangesAsync();

            var controller = new BooksController(db);
            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var related = viewResult.ViewData["Related"] as IEnumerable<Book>;

            Assert.NotNull(related);
            Assert.DoesNotContain(related, b => b.Id == 1);
        }

        [Fact]
        public async Task Details_ShouldReturnRelatedBooksBySimilarRating()
        {
            var db = GetInMemoryDb();
            db.Books.AddRange(
                new Book { Id = 1, Title = "Book A", Author = "Author", Genre = "Fantasy", Rating = 4.5 },
                new Book { Id = 2, Title = "Book B", Author = "Author", Genre = "Horror", Rating = 4.3 },
                new Book { Id = 3, Title = "Book C", Author = "Author", Genre = "Romance", Rating = 1.0 }
            );
            await db.SaveChangesAsync();

            var controller = new BooksController(db);
            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var related = viewResult.ViewData["Related"] as IEnumerable<Book>;

            Assert.NotNull(related);
            Assert.Contains(related, b => b.Id == 2);
        }

        [Fact]
        public async Task Index_EmptyDatabase_ReturnsEmptyList()
        {
            var db = GetInMemoryDb();
            var controller = new BooksController(db);
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.Model);
            Assert.Empty(books);
        }

        [Fact]
        public async Task Add_InvalidModel_ReturnsView()
        {
            var db = GetInMemoryDb();
            var controller = new BooksController(db);
            controller.ModelState.AddModelError("Title", "Required");

            var book = new Book();
            var result = await controller.Add(book);

            Assert.IsType<ViewResult>(result);
            Assert.Equal(0, db.Books.Count());
        }
    }
}