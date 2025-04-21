using ECommerce.Application.DTO.Identity;
using ECommerce.Domain.Identity;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> RegisterUser(RegisterDTO registerDTO);
        Task<(AuthenticationResponse, string)> LoginUser(LoginDTO loginDTO);
        Task LogoutUser();
    }
}
