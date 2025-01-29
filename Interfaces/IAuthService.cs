using Entities;
using Interfaces.DTO.Auth;

namespace Interfaces
{
    public interface IAuthService
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user, string hashedPassword, string providedPassword);
        Task<string> LoginUser(LoginUserDTO loginUserDTO);
    }
}
