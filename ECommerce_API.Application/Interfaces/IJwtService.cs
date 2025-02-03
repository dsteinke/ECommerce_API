using ECommerce_API.Core.Identity;

namespace ECommerce_API.Application.Interfaces
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
        string CreateRefreshToken();
        Task<(AuthenticationResponse, string)> GetRefreshToken(string refreshToken);
    }
}
