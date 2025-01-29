using Entities;

namespace RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(User user);
        Task<User?> GetUserById(Guid userId);
        Task<User?> GetUserByUsernameOrEmail(string usernameOrPassword);
        Task<bool> UserExists(string username, string email);
    }
}
