using Xunit;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using reco_system.Controllers;
using reco_system.Data;
using reco_system.Models;

namespace reco_system.Tests
{
    public class BooksControllerTests
    {
        private ApplicationDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsViewWithBooks()
        {
            var db = GetInMemoryDb();
            db.Books.Add(new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian", Rating = 4.7 });
            db.Books.Add(new Book { Title = "The Hobbit", Author = "Tolkien", Genre = "Fantasy", Rating = 4.8 });
            await db.SaveChangesAsync();

            var controller = new BooksController(db);
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.Model);
            Assert.Equal(2, books.Count());
        }

        [Fact]
        public async Task Details_ReturnsNotFound_WhenBookDoesNotExist()
        {
            var db = GetInMemoryDb();
            var controller = new BooksController(db);

            var result = await controller.Details(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_ReturnsViewWithBook_WhenBookExists()
        {
            var db = GetInMemoryDb();
            db.Books.Add(new Book { Id = 1, Title = "1984", Author = "Orwell", Genre = "Dystopian", Rating = 4.7 });
            await db.SaveChangesAsync();

            var controller = new BooksController(db);
            var result = await controller.Details(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var book = Assert.IsType<Book>(viewResult.Model);
            Assert.Equal("1984", book.Title);
        }

        [Fact]
        public async Task Add_Post_SavesBookAndRedirects()
        {
            var db = GetInMemoryDb();
            var controller = new BooksController(db);
            var book = new Book { Title = "New Book", Author = "Author", Genre = "Fiction", Rating = 4.0 };

            var result = await controller.Add(book);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Equal(1, db.Books.Count());
        }

        [Fact]
        public async Task Delete_Get_ReturnsViewWithBook()
        {
            var db = GetInMemoryDb();
            db.Books.Add(new Book { Id = 1, Title = "To Delete", Author = "Author", Genre = "Fiction", Rating = 3.0 });
            await db.SaveChangesAsync();

            var controller = new BooksController(db);
            var result = await controller.Delete(1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var book = Assert.IsType<Book>(viewResult.Model);
            Assert.Equal("To Delete", book.Title);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenBookDoesNotExist()
        {
            var db = GetInMemoryDb();
            var controller = new BooksController(db);

            var result = await controller.Delete(999);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteConfirmed_RemovesBookAndRedirects()
        {
            var db = GetInMemoryDb();
            db.Books.Add(new Book { Id = 1, Title = "To Delete", Author = "Author", Genre = "Fiction", Rating = 3.0 });
            await db.SaveChangesAsync();

            var controller = new BooksController(db);
            var result = await controller.DeleteConfirmed(1);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
            Assert.Equal(0, db.Books.Count());
        }

        [Fact]
        public async Task Edit_Post_UpdatesBookAndRedirects()
        {
            var db = GetInMemoryDb();
            db.Books.Add(new Book { Id = 1, Title = "Old Title", Author = "Author", Genre = "Fiction", Rating = 3.0 });
            await db.SaveChangesAsync();

            db.ChangeTracker.Clear();

            var controller = new BooksController(db);
            var updatedBook = new Book { Id = 1, Title = "New Title", Author = "Author", Genre = "Fiction", Rating = 4.5 };
            var result = await controller.Edit(updatedBook);

            var redirect = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirect.ActionName);
        }
    }
}