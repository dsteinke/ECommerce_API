using Interfaces.DTO.Auth;
using Interfaces.DTO.User;

namespace Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(UserAddDTO userAddDTO);
        Task<UserResponseDTO> GetUserById(Guid userId);
    }
}
