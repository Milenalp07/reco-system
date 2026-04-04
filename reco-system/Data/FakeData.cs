using reco_system.Models;

namespace reco_system.Data
{
    public static class FakeData
    {
        public static List<Book> Books => new()
        {
            new Book
            {
                Id = 1,
                Title = "The Hobbit",
                Author = "J.R.R. Tolkien",
                Genre = "Fantasy",
                YearPublished = 1937,
                Description = "A fantasy adventure novel about Bilbo Baggins.",
                Rating = 4.8,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1546071216i/5907.jpg"
            },
            new Book
            {
                Id = 2,
                Title = "Pride and Prejudice",
                Author = "Jane Austen",
                Genre = "Romance",
                YearPublished = 1813,
                Description = "A classic story about love, class, and misunderstandings.",
                Rating = 4.6,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1320399351i/1885.jpg"
            },
            new Book
            {
                Id = 3,
                Title = "Atomic Habits",
                Author = "James Clear",
                Genre = "Self-Help",
                YearPublished = 2018,
                Description = "A practical guide to building good habits and breaking bad ones.",
                Rating = 4.7,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1655988385i/40121378.jpg"
            }
        };

        public static List<Movie> Movies => new()
        {
            new Movie
            {
                Id = 1,
                Title = "Inception",
                Genre = "Sci-Fi",
                ReleaseYear = 2010,
                Description = "A thief enters dreams to steal secrets.",
                Rating = 4.8,
                ImageUrl = "https://media.themoviedb.org/t/p/w600_and_h900_face/xlaY2zyzMfkhk0HSC5VUwzoZPU1.jpg"
            },
            new Movie
            {
                Id = 2,
                Title = "Interstellar",
                Genre = "Sci-Fi",
                ReleaseYear = 2014,
                Description = "A team travels through a wormhole to save humanity.",
                Rating = 4.9,
                ImageUrl = "https://image.tmdb.org/t/p/w500/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"
            },
            new Movie
            {
                Id = 3,
                Title = "The Notebook",
                Genre = "Romance",
                ReleaseYear = 2004,
                Description = "A romantic story about enduring love.",
                Rating = 4.4,
                ImageUrl = "https://image.tmdb.org/t/p/w500/rNzQyW4f8B8cQeg7Dgj3n6eT5k9.jpg"
            }
        };
    }
}