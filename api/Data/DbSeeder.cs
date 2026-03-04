using api.Models;

namespace api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext db)
    {
        // Movies
        if (!db.Movies.Any())
        {
            db.Movies.AddRange(
                new Movie
                {
                    Title = "Inception",
                    Genre = "Sci-Fi",
                    ReleaseYear = 2010,
                    Description = "A thief who steals corporate secrets through dream-sharing technology.",
                    Rating = 4.8
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    Genre = "Action",
                    ReleaseYear = 2008,
                    Description = "Batman faces the Joker in Gotham City.",
                    Rating = 4.9
                },
                new Movie
                {
                    Title = "Interstellar",
                    Genre = "Sci-Fi",
                    ReleaseYear = 2014,
                    Description = "A team travels through a wormhole in space to save humanity.",
                    Rating = 4.7
                }
            );
        }

        // Books
        if (!db.Books.Any())
        {
            db.Books.AddRange(
                new Book
                {
                    Title = "1984",
                    Author = "George Orwell",
                    Genre = "Dystopian",
                    YearPublished = 1949,
                    Description = "A dystopian novel about totalitarian regime."
                },
                new Book
                {
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Genre = "Fantasy",
                    YearPublished = 1937,
                    Description = "A hobbit goes on an unexpected adventure."
                },
                new Book
                {
                    Title = "To Kill a Mockingbird",
                    Author = "Harper Lee",
                    Genre = "Fiction",
                    YearPublished = 1960,
                    Description = "A story about racial injustice in the Deep South."
                }
            );
        }

        db.SaveChanges();
    }
}