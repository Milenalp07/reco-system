using Microsoft.AspNetCore.Identity;

namespace reco_system.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}