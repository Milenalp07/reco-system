using Microsoft.AspNetCore.Identity;

namespace reco_system.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
        public string Plan { get; set; } = "Free";
        public string? FavoriteGenre { get; set; }
        public string? PreferredLanguage { get; set; } = "English";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}