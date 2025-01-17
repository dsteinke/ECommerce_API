using Entities;
using Interfaces;
using Interfaces.DTO.User;
using Microsoft.AspNetCore.Identity;
using RepositoryInterfaces;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public UserService
            (IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task RegisterUser(UserAddDTO userAddDTO)
        {
            //Check if user already exists
            if (await _userRepository.UserExists(userAddDTO.Username, userAddDTO.Email) == false)
                throw new InvalidOperationException("User with the same username or email already exists");

            var user = userAddDTO.ToUser();

            //create cart for user
            var cart = new Cart
            {
                CartId = Guid.NewGuid(),
                UserId = user.UserId,
                User = user,
                CartItems = new List<CartItem>()
            };

            user.PasswordHash = HashPassword(user, userAddDTO.PasswordHash);
            user.Cart = cart;

            await _userRepository.RegisterUser(user);
        }

        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyPassword(User user, string hashedPassword, string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);

            return result == PasswordVerificationResult.Success;
        }
    }
}
