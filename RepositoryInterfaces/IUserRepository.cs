using Entities;

namespace RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(User user);
        Task<bool> UserExists(string username, string email);

    }
}
