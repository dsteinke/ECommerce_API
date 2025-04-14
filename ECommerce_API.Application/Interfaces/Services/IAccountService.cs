using ECommerce.Application.DTO.Identity;
using ECommerce.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> RegisterUser(RegisterDTO registerDTO);
        Task<(AuthenticationResponse, string)> LoginUser(LoginDTO loginDTO);
        Task LogoutUser();
    }
}
