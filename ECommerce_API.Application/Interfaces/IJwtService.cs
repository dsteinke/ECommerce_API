using ECommerce_API.Core;
using ECommerce_API.Core.Identity;

namespace ECommerce_API.Application.Interfaces
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
    }
}
