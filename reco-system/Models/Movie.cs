using System.ComponentModel.DataAnnotations;

namespace reco_system.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        public int ReleaseYear { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0, 5)]
        public double Rating { get; set; }

        public string? ImageUrl { get; set; }
    }
}