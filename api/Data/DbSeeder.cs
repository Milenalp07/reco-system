using api.Models;

namespace api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Books.Any())
        {
            var books = new List<Book>
            {
                new Book
                {
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Genre = "Fantasy",
                    YearPublished = 1937,
                    Description = "Adventure in Middle-earth",
                    Rating = 4.8
                },
                new Book
                {
                    Title = "Harry Potter",
                    Author = "J.K. Rowling",
                    Genre = "Fantasy",
                    YearPublished = 1997,
                    Description = "Wizard story",
                    Rating = 4.9
                }
            };

            context.Books.AddRange(books);
        }

        context.SaveChanges();
    }
}