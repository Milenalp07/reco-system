using reco_system.Models;
using Xunit;

namespace reco_system.Tests.Models
{
    public class BookTests
    {
        [Fact]
        public void Book_Should_Create_With_Correct_Properties()
        {
            var book = new Book
            {
                Id = 1,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy",
                YearPublished = 1937,
                Description = "A fantasy novel",
                Rating = 4.8,
                ImageUrl = "/images/hobbit.jpg"
            };

            Assert.Equal(1, book.Id);
            Assert.Equal("The Hobbit", book.Title);
            Assert.Equal("J.R.R. Tolkien", book.Author);
            Assert.Equal("Fantasy", book.Genre);
            Assert.Equal(1937, book.YearPublished);
            Assert.Equal("A fantasy novel", book.Description);
            Assert.Equal(4.8, book.Rating);
            Assert.Equal("/images/hobbit.jpg", book.ImageUrl);
        }
    }
}