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
                new Book { Title = "The Hobbit", Author = "J.R.R. Tolkien", Genre = "Fantasy", YearPublished = 1937, Description = "A fantasy adventure novel.", Rating = 4.8, Isbn = "9780547928227" },
                new Book { Title = "The Lord of the Rings", Author = "J.R.R. Tolkien", Genre = "Fantasy", YearPublished = 1954, Description = "Epic high-fantasy novel.", Rating = 4.9, Isbn = "9780618640157" },
                new Book { Title = "Harry Potter and the Sorcerer's Stone", Author = "J.K. Rowling", Genre = "Fantasy", YearPublished = 1997, Description = "A young wizard's journey begins.", Rating = 4.7, Isbn = "9780590353427" },
                new Book { Title = "Harry Potter and the Chamber of Secrets", Author = "J.K. Rowling", Genre = "Fantasy", YearPublished = 1998, Description = "Harry returns to Hogwarts for another mystery.", Rating = 4.6, Isbn = "9780439064873" },
                new Book { Title = "Harry Potter and the Prisoner of Azkaban", Author = "J.K. Rowling", Genre = "Fantasy", YearPublished = 1999, Description = "Harry discovers more about his past.", Rating = 4.8, Isbn = "9780439136365" },

                new Book { Title = "1984", Author = "George Orwell", Genre = "Dystopian", YearPublished = 1949, Description = "A novel about surveillance and totalitarianism.", Rating = 4.7, Isbn = "9780451524935" },
                new Book { Title = "Animal Farm", Author = "George Orwell", Genre = "Political Satire", YearPublished = 1945, Description = "A satirical allegory about power and corruption.", Rating = 4.5, Isbn = "9780451526342" },
                new Book { Title = "Brave New World", Author = "Aldous Huxley", Genre = "Dystopian", YearPublished = 1932, Description = "A futuristic society shaped by control and conditioning.", Rating = 4.5, Isbn = "9780060850524" },
                new Book { Title = "Fahrenheit 451", Author = "Ray Bradbury", Genre = "Sci-Fi", YearPublished = 1953, Description = "A society where books are forbidden and burned.", Rating = 4.4, Isbn = "9781451673319" },
                new Book { Title = "The Hunger Games", Author = "Suzanne Collins", Genre = "Dystopian", YearPublished = 2008, Description = "A deadly competition in a dystopian world.", Rating = 4.6, Isbn = "9780439023481" },

                new Book { Title = "Catching Fire", Author = "Suzanne Collins", Genre = "Dystopian", YearPublished = 2009, Description = "Katniss faces greater danger and rebellion grows.", Rating = 4.7, Isbn = "9780439023498" },
                new Book { Title = "Mockingjay", Author = "Suzanne Collins", Genre = "Dystopian", YearPublished = 2010, Description = "The final fight against oppression.", Rating = 4.5, Isbn = "9780439023511" },
                new Book { Title = "Dune", Author = "Frank Herbert", Genre = "Sci-Fi", YearPublished = 1965, Description = "Power, prophecy, and survival on Arrakis.", Rating = 4.7, Isbn = "9780441172719" },
                new Book { Title = "Foundation", Author = "Isaac Asimov", Genre = "Sci-Fi", YearPublished = 1951, Description = "The fall and rebuilding of a galactic empire.", Rating = 4.6, Isbn = "9780553293357" },
                new Book { Title = "Neuromancer", Author = "William Gibson", Genre = "Sci-Fi", YearPublished = 1984, Description = "A defining cyberpunk classic.", Rating = 4.3, Isbn = "9780441569595" },

                new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger", Genre = "Classic", YearPublished = 1951, Description = "A story of teenage alienation and identity.", Rating = 4.2, Isbn = "9780316769488" },
                new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Classic", YearPublished = 1960, Description = "A powerful novel about justice and race.", Rating = 4.8, Isbn = "9780061120084" },
                new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic", YearPublished = 1925, Description = "A tragic portrait of wealth and illusion.", Rating = 4.3, Isbn = "9780743273565" },
                new Book { Title = "Moby-Dick", Author = "Herman Melville", Genre = "Classic", YearPublished = 1851, Description = "A whaling voyage and obsession.", Rating = 4.1, Isbn = "9780142437247" },
                new Book { Title = "Jane Eyre", Author = "Charlotte Bronte", Genre = "Classic", YearPublished = 1847, Description = "A story of resilience, love, and independence.", Rating = 4.5, Isbn = "9780142437209" },

                new Book { Title = "Pride and Prejudice", Author = "Jane Austen", Genre = "Romance", YearPublished = 1813, Description = "A classic novel about love and society.", Rating = 4.6, Isbn = "9780141439518" },
                new Book { Title = "Sense and Sensibility", Author = "Jane Austen", Genre = "Romance", YearPublished = 1811, Description = "The lives and loves of two sisters.", Rating = 4.3, Isbn = "9780141439662" },
                new Book { Title = "Me Before You", Author = "Jojo Moyes", Genre = "Romance", YearPublished = 2012, Description = "A moving story about love and life choices.", Rating = 4.5, Isbn = "9780143124542" },
                new Book { Title = "The Fault in Our Stars", Author = "John Green", Genre = "Romance", YearPublished = 2012, Description = "A heartfelt story of love and illness.", Rating = 4.4, Isbn = "9780525478812" },
                new Book { Title = "Twilight", Author = "Stephenie Meyer", Genre = "Fantasy", YearPublished = 2005, Description = "A supernatural love story.", Rating = 4.0, Isbn = "9780316015844" },

                new Book { Title = "New Moon", Author = "Stephenie Meyer", Genre = "Fantasy", YearPublished = 2006, Description = "Love, loss, and werewolves.", Rating = 4.1, Isbn = "9780316160193" },
                new Book { Title = "Eclipse", Author = "Stephenie Meyer", Genre = "Fantasy", YearPublished = 2007, Description = "The battle between love and danger intensifies.", Rating = 4.1, Isbn = "9780316160209" },
                new Book { Title = "The Da Vinci Code", Author = "Dan Brown", Genre = "Thriller", YearPublished = 2003, Description = "A mystery involving symbols and secret societies.", Rating = 4.4, Isbn = "9780307474278" },
                new Book { Title = "Angels & Demons", Author = "Dan Brown", Genre = "Thriller", YearPublished = 2000, Description = "A thrilling race against time in Rome.", Rating = 4.3, Isbn = "9780743493468" },
                new Book { Title = "Inferno", Author = "Dan Brown", Genre = "Thriller", YearPublished = 2013, Description = "A fast-paced thriller inspired by Dante.", Rating = 4.2, Isbn = "9780804172264" },

                new Book { Title = "Gone Girl", Author = "Gillian Flynn", Genre = "Thriller", YearPublished = 2012, Description = "A twisted psychological thriller.", Rating = 4.3, Isbn = "9780307588371" },
                new Book { Title = "The Silent Patient", Author = "Alex Michaelides", Genre = "Thriller", YearPublished = 2019, Description = "A psychological mystery about a silent woman.", Rating = 4.4, Isbn = "9781250301697" },
                new Book { Title = "Verity", Author = "Colleen Hoover", Genre = "Thriller", YearPublished = 2018, Description = "Dark secrets and unsettling truths.", Rating = 4.3, Isbn = "9781538724736" },
                new Book { Title = "The Girl with the Dragon Tattoo", Author = "Stieg Larsson", Genre = "Crime", YearPublished = 2005, Description = "A journalist and hacker investigate a disappearance.", Rating = 4.4, Isbn = "9780307454546" },
                new Book { Title = "The Reversal", Author = "Michael Connelly", Genre = "Crime", YearPublished = 2010, Description = "A legal thriller centered on a reopened case.", Rating = 4.1, Isbn = "9780316069430" },

                new Book { Title = "It", Author = "Stephen King", Genre = "Horror", YearPublished = 1986, Description = "A terrifying evil haunts a small town.", Rating = 4.4, Isbn = "9781501142970" },
                new Book { Title = "The Shining", Author = "Stephen King", Genre = "Horror", YearPublished = 1977, Description = "A haunted hotel and a family in isolation.", Rating = 4.5, Isbn = "9780307743657" },
                new Book { Title = "Misery", Author = "Stephen King", Genre = "Horror", YearPublished = 1987, Description = "An author is trapped by an obsessive fan.", Rating = 4.3, Isbn = "9781501143106" },
                new Book { Title = "Dracula", Author = "Bram Stoker", Genre = "Horror", YearPublished = 1897, Description = "The classic tale of Count Dracula.", Rating = 4.2, Isbn = "9780141439846" },
                new Book { Title = "Frankenstein", Author = "Mary Shelley", Genre = "Horror", YearPublished = 1818, Description = "A scientist creates life with tragic consequences.", Rating = 4.1, Isbn = "9780141439471" },

                new Book { Title = "The Book Thief", Author = "Markus Zusak", Genre = "Historical", YearPublished = 2005, Description = "A young girl discovers the power of books in Nazi Germany.", Rating = 4.8, Isbn = "9780375842207" },
                new Book { Title = "All the Light We Cannot See", Author = "Anthony Doerr", Genre = "Historical", YearPublished = 2014, Description = "A WWII story of survival and connection.", Rating = 4.7, Isbn = "9781501173219" },
                new Book { Title = "The Nightingale", Author = "Kristin Hannah", Genre = "Historical", YearPublished = 2015, Description = "Two sisters face WWII in occupied France.", Rating = 4.8, Isbn = "9781250080400" },
                new Book { Title = "The Kite Runner", Author = "Khaled Hosseini", Genre = "Drama", YearPublished = 2003, Description = "Friendship, guilt, and redemption.", Rating = 4.7, Isbn = "9781594631931" },
                new Book { Title = "A Thousand Splendid Suns", Author = "Khaled Hosseini", Genre = "Drama", YearPublished = 2007, Description = "The lives of two Afghan women intertwine.", Rating = 4.8, Isbn = "9781594483851" },

                new Book { Title = "The Alchemist", Author = "Paulo Coelho", Genre = "Adventure", YearPublished = 1988, Description = "A journey of destiny and self-discovery.", Rating = 4.5, Isbn = "9780061122415" },
                new Book { Title = "Life of Pi", Author = "Yann Martel", Genre = "Adventure", YearPublished = 2001, Description = "A boy survives at sea with a tiger.", Rating = 4.4, Isbn = "9780156027328" },
                new Book { Title = "The Martian", Author = "Andy Weir", Genre = "Sci-Fi", YearPublished = 2011, Description = "An astronaut struggles to survive on Mars.", Rating = 4.7, Isbn = "9780553418026" },
                new Book { Title = "Atomic Habits", Author = "James Clear", Genre = "Self-Help", YearPublished = 2018, Description = "Practical strategies for building good habits.", Rating = 4.8, Isbn = "9780735211292" },
                new Book { Title = "Rich Dad Poor Dad", Author = "Robert Kiyosaki", Genre = "Finance", YearPublished = 1997, Description = "Lessons about money, assets, and mindset.", Rating = 4.5, Isbn = "9781612680194" },

                new Book { Title = "Think and Grow Rich", Author = "Napoleon Hill", Genre = "Finance", YearPublished = 1937, Description = "A classic book on success principles.", Rating = 4.4, Isbn = "9781585424337" },
                new Book { Title = "Sapiens", Author = "Yuval Noah Harari", Genre = "History", YearPublished = 2011, Description = "A brief history of humankind.", Rating = 4.7, Isbn = "9780062316097" },
                new Book { Title = "Homo Deus", Author = "Yuval Noah Harari", Genre = "History", YearPublished = 2015, Description = "A look at the future of humanity.", Rating = 4.6, Isbn = "9780062464316" },
                new Book { Title = "Educated", Author = "Tara Westover", Genre = "Memoir", YearPublished = 2018, Description = "A memoir about education and transformation.", Rating = 4.7, Isbn = "9780399590504" },
                new Book { Title = "Becoming", Author = "Michelle Obama", Genre = "Memoir", YearPublished = 2018, Description = "A memoir by the former First Lady.", Rating = 4.8, Isbn = "9781524763138" }
            );

            context.SaveChanges();
        }
    }
}