using ECommerce_API.Application.DTO.Identity;
using ECommerce_API.Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerce_API.Application.Interfaces
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> RegisterUser(RegisterDTO registerDTO);
        Task<(AuthenticationResponse, string)> LoginUser(LoginDTO loginDTO);
        Task LogoutUser();
    }
}
