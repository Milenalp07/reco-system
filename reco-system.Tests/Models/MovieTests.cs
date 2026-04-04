using reco_system.Models;
using Xunit;

namespace reco_system.Tests.Models
{
    public class MovieTests
    {
        [Fact]
        public void Movie_Should_Create_With_Correct_Properties()
        {
            var movie = new Movie
            {
                Id = 1,
                Title = "Interstellar",
                Genre = "Sci-Fi",
                ReleaseYear = 2014,
                Description = "A space exploration film",
                Rating = 4.9,
                ImageUrl = "/images/interstellar.jpg"
            };

            Assert.Equal(1, movie.Id);
            Assert.Equal("Interstellar", movie.Title);
            Assert.Equal("Sci-Fi", movie.Genre);
            Assert.Equal(2014, movie.ReleaseYear);
            Assert.Equal("A space exploration film", movie.Description);
            Assert.Equal(4.9, movie.Rating);
            Assert.Equal("/images/interstellar.jpg", movie.ImageUrl);
        }
    }
}