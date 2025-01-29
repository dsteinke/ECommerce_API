using ECommerce_API.Core;

namespace ECommerce_API.Application
{
    public interface IUserRepository
    {
        Task RegisterUser(User user);
        Task<User?> GetUserById(Guid userId);
        Task<User?> GetUserByUsernameOrEmail(string usernameOrPassword);
        Task<bool> UserExists(string username, string email);
    }
}
