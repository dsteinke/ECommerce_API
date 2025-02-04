using Microsoft.AspNetCore.Identity;

namespace ECommerce_API.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public Cart Cart { get; set; }
    }
}
