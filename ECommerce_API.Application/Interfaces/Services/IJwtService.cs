using ECommerce.Domain.Identity;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(ApplicationUser user);
        string CreateRefreshToken();
        Task<(AuthenticationResponse, string)> GetRefreshToken(string refreshToken);
    }
}
