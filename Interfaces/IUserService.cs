using Interfaces.DTO.User;

namespace Interfaces
{
    public interface IUserService
    {
        Task RegisterUser(UserAddDTO userAddDTO);
    }
}
