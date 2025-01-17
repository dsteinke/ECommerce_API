using Entities;

namespace RepositoryInterfaces
{
    public interface IUserRepository
    {
        Task RegisterUser(User user);

    }
}
