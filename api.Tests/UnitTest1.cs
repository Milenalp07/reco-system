using api.Data;
using api.Models;
using api.Services;
using Microsoft.EntityFrameworkCore;

namespace api.Tests;

public class RecommendationServiceTests
{
    private AppDbContext CreateDbContext(string dbName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new AppDbContext(options);

        context.Movies.AddRange(
            new Movie { Id = 1, Title = "Inception",       Genre = "Sci-Fi",  Rating = 4.8, ReleaseYear = 2010 },
            new Movie { Id = 2, Title = "The Matrix",      Genre = "Sci-Fi",  Rating = 4.7, ReleaseYear = 1999 },
            new Movie { Id = 3, Title = "The Notebook",    Genre = "Romance", Rating = 4.2, ReleaseYear = 2004 },
            new Movie { Id = 4, Title = "Interstellar",    Genre = "Sci-Fi",  Rating = 4.9, ReleaseYear = 2014 },
            new Movie { Id = 5, Title = "Titanic",         Genre = "Romance", Rating = 4.5, ReleaseYear = 1997 },
            new Movie { Id = 6, Title = "Low Rated Movie", Genre = "Sci-Fi",  Rating = 2.0, ReleaseYear = 2000 }
        );

        context.Books.AddRange(
            new Book { Id = 1, Title = "The Hobbit",      Author = "J.R.R. Tolkien", Genre = "Fantasy",  Rating = 4.8, YearPublished = 1937 },
            new Book { Id = 2, Title = "Harry Potter",    Author = "J.K. Rowling",   Genre = "Fantasy",  Rating = 4.9, YearPublished = 1997 },
            new Book { Id = 3, Title = "1984",            Author = "George Orwell",  Genre = "Dystopia", Rating = 4.7, YearPublished = 1949 },
            new Book { Id = 4, Title = "Brave New World", Author = "A. Huxley",      Genre = "Dystopia", Rating = 4.3, YearPublished = 1932 },
            new Book { Id = 5, Title = "Bad Book",        Author = "Unknown",        Genre = "Fantasy",  Rating = 1.5, YearPublished = 2000 }
        );

        context.SaveChanges();
        return context;
    }

    [Fact]
    public async Task GetMovieRecommendations_NoFilters_ReturnsTop5ByRating()
    {
        var context = CreateDbContext("movies_no_filter");
        var service = new RecommendationService(context);

        var result = await service.GetMovieRecommendations(null, null);

        Assert.NotNull(result);
        Assert.True(result.Count <= 5);
        Assert.Equal("Interstellar", result.First().Title);
    }

    [Fact]
    public async Task GetMovieRecommendations_ByGenre_ReturnsOnlyThatGenre()
    {
        var context = CreateDbContext("movies_by_genre");
        var service = new RecommendationService(context);

        var result = await service.GetMovieRecommendations("Romance", null);

        Assert.NotNull(result);
        Assert.All(result, m => Assert.Equal("Romance", m.Genre));
    }

    [Fact]
    public async Task GetMovieRecommendations_ByMinRating_ExcludesLowRated()
    {
        var context = CreateDbContext("movies_by_rating");
        var service = new RecommendationService(context);

        var result = await service.GetMovieRecommendations(null, 4.0);

        Assert.NotNull(result);
        Assert.All(result, m => Assert.True(m.Rating >= 4.0));
        Assert.DoesNotContain(result, m => m.Title == "Low Rated Movie");
    }

    [Fact]
    public async Task GetMovieRecommendations_UnknownGenre_ReturnsEmptyList()
    {
        var context = CreateDbContext("movies_unknown_genre");
        var service = new RecommendationService(context);

        var result = await service.GetMovieRecommendations("Horror", null);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetMovieRecommendations_ResultsAreOrderedByRatingDescending()
    {
        var context = CreateDbContext("movies_ordered");
        var service = new RecommendationService(context);

        var result = await service.GetMovieRecommendations(null, null);

        for (int i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].Rating >= result[i + 1].Rating);
        }
    }

    [Fact]
    public async Task GetBookRecommendations_NoFilters_ReturnsTop5ByRating()
    {
        var context = CreateDbContext("books_no_filter");
        var service = new RecommendationService(context);

        var result = await service.GetBookRecommendations(null, null);

        Assert.NotNull(result);
        Assert.True(result.Count <= 5);
        Assert.Equal("Harry Potter", result.First().Title);
    }

    [Fact]
    public async Task GetBookRecommendations_ByGenre_ReturnsOnlyThatGenre()
    {
        var context = CreateDbContext("books_by_genre");
        var service = new RecommendationService(context);

        var result = await service.GetBookRecommendations("Dystopia", null);

        Assert.NotNull(result);
        Assert.All(result, b => Assert.Equal("Dystopia", b.Genre));
    }

    [Fact]
    public async Task GetBookRecommendations_ByMinRating_ExcludesLowRated()
    {
        var context = CreateDbContext("books_by_rating");
        var service = new RecommendationService(context);

        var result = await service.GetBookRecommendations(null, 4.0);

        Assert.NotNull(result);
        Assert.All(result, b => Assert.True(b.Rating >= 4.0));
        Assert.DoesNotContain(result, b => b.Title == "Bad Book");
    }

    [Fact]
    public async Task GetBookRecommendations_UnknownGenre_ReturnsEmptyList()
    {
        var context = CreateDbContext("books_unknown_genre");
        var service = new RecommendationService(context);

        var result = await service.GetBookRecommendations("Romance", null);

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetBookRecommendations_ResultsAreOrderedByRatingDescending()
    {
        var context = CreateDbContext("books_ordered");
        var service = new RecommendationService(context);

        var result = await service.GetBookRecommendations(null, null);

        for (int i = 0; i < result.Count - 1; i++)
        {
            Assert.True(result[i].Rating >= result[i + 1].Rating);
        }
    }
}