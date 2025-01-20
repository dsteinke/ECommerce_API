using AutoMapper;
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
        private readonly IMapper _mapper;


        public UserService
            (IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
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

        public async Task<UserResponseDTO> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                throw new KeyNotFoundException($"No user found with userId: {userId}");

            var result = _mapper.Map<UserResponseDTO>(user);

            return result;
        }
    }
}
