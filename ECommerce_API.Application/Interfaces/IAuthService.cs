using ECommerce_API.Core;

namespace ECommerce_API.Application
{
    public interface IAuthService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string providedPassword);
        Task<string> LoginUser(LoginUserDTO loginUserDTO);
    }
}
