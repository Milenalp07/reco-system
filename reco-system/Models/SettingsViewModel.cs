using System.ComponentModel.DataAnnotations;

namespace reco_system.Models
{
    public class SettingsViewModel
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        public string? AvatarUrl { get; set; }
        public string? FavoriteGenre { get; set; }
        public string? PreferredLanguage { get; set; }
        public string? Plan { get; set; }

        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        [MinLength(6)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string? ConfirmNewPassword { get; set; }
    }
}