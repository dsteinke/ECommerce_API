using Microsoft.AspNetCore.Identity;

namespace ECommerce_API.Core.Identity
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public Cart Cart { get; set; }
    }
}
