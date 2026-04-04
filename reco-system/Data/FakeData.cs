using reco_system.Models;

namespace reco_system.Data
{
    public static class FakeData
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Books.Any())
                return;

            context.Books.AddRange(
                new Book
                {
                    Title = "The Hobbit",
                    Author = "J.R.R. Tolkien",
                    Genre = "Fantasy",
                    YearPublished = 1937,
                    Description = "A fantasy adventure novel.",
                    Rating = 4.8,
                },
                new Book
                {
                    Title = "1984",
                    Author = "George Orwell",
                    Genre = "Dystopian",
                    YearPublished = 1949,
                    Description = "A novel about surveillance and totalitarianism.",
                    Rating = 4.7,
                },
                new Book
                {
                    Title = "Pride and Prejudice",
                    Author = "Jane Austen",
                    Genre = "Romance",
                    YearPublished = 1813,
                    Description = "A classic novel about love and society.",
                    Rating = 4.6,
                }
            );

            context.SaveChanges();
        }
    }
}