namespace api.Models;

public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string Genre { get; set; } = string.Empty;

    public int YearPublished { get; set; }

    public string Description { get; set; } = string.Empty;

    // Average rating
    public double Rating { get; set; }
}