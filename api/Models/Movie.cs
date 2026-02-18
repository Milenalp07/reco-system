namespace api.Models;

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public int ReleaseYear { get; set; }

    public string? Description { get; set; }

    // Average rating (can be calculated later from Ratings table)
    public double Rating { get; set; }
}
