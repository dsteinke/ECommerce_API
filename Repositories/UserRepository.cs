using Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User?> GetUserById(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

            return user;
        }

        public async Task RegisterUser(User user)
        {
            _context.Users.Add(user);

            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExists(string username, string email)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Username == username || x.Email == email);

            return userExists;
        }


    }
}
