using reco_system.Data;
using Xunit;

namespace reco_system.Tests.Data
{
    public class FakeDataTests
    {
        [Fact]
        public void Books_Should_Not_Be_Empty()
        {
            var books = FakeData.Books;

            Assert.NotNull(books);
            Assert.NotEmpty(books);
        }

        [Fact]
        public void Movies_Should_Not_Be_Empty()
        {
            var movies = FakeData.Movies;

            Assert.NotNull(movies);
            Assert.NotEmpty(movies);
        }

        [Fact]
        public void Books_Should_Have_Unique_Ids()
        {
            var books = FakeData.Books;
            var distinctIds = books.Select(b => b.Id).Distinct().Count();

            Assert.Equal(books.Count, distinctIds);
        }

        [Fact]
        public void Movies_Should_Have_Unique_Ids()
        {
            var movies = FakeData.Movies;
            var distinctIds = movies.Select(m => m.Id).Distinct().Count();

            Assert.Equal(movies.Count, distinctIds);
        }
    }
}