using Xunit;
using reco_system.Models;

namespace reco_system.Tests
{
    public class BookModelTests
    {
        [Fact]
        public void Book_ShouldHaveCorrectTitle()
        {
            var book = new Book { Title = "The Hobbit" };
            Assert.Equal("The Hobbit", book.Title);
        }

        [Fact]
        public void Book_ShouldHaveCorrectAuthor()
        {
            var book = new Book { Author = "J.R.R. Tolkien" };
            Assert.Equal("J.R.R. Tolkien", book.Author);
        }

        [Fact]
        public void Book_RatingShouldBeWithinRange()
        {
            var book = new Book { Rating = 4.5 };
            Assert.InRange(book.Rating, 0, 5);
        }

        [Fact]
        public void Book_RatingShouldNotExceedFive()
        {
            var book = new Book { Rating = 5.0 };
            Assert.True(book.Rating <= 5.0);
        }

        [Fact]
        public void Book_DefaultDescriptionShouldBeNull()
        {
            var book = new Book();
            Assert.Null(book.Description);
        }

        [Fact]
        public void Book_ShouldStoreGenreCorrectly()
        {
            var book = new Book { Genre = "Fantasy" };
            Assert.Equal("Fantasy", book.Genre);
        }

        [Fact]
        public void Book_ShouldStoreYearPublished()
        {
            var book = new Book { YearPublished = 1937 };
            Assert.Equal(1937, book.YearPublished);
        }
    }
}