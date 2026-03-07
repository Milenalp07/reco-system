using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class RecommendationService
{
    private readonly AppDbContext _context;

    public RecommendationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Movie>> GetMovieRecommendations(string? genre, double? minRating)
    {
        var query = _context.Movies.AsQueryable();

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(m => m.Genre == genre);
        }

        if (minRating.HasValue)
        {
            query = query.Where(m => m.Rating >= minRating.Value);
        }

        return await query
            .OrderByDescending(m => m.Rating)
            .Take(5)
            .ToListAsync();
    }

    public async Task<List<Book>> GetBookRecommendations(string? genre, double? minRating)
    {
        var query = _context.Books.AsQueryable();

        if (!string.IsNullOrEmpty(genre))
        {
            query = query.Where(b => b.Genre == genre);
        }

        if (minRating.HasValue)
        {
            query = query.Where(b => b.Rating >= minRating.Value);
        }

        return await query
            .OrderByDescending(b => b.Rating)
            .Take(5)
            .ToListAsync();
    }
}