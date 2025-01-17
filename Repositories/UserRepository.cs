using Entities;
using Interfaces.DTO.User;
using RepositoryInterfaces;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ECommerceDbContext _context;

        public UserRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task RegisterUser(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

    }
}
