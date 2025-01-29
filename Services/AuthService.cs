using Entities;
using Interfaces;
using Interfaces.DTO.Auth;
using Microsoft.AspNetCore.Identity;
using RepositoryInterfaces;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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

        public async Task<string> LoginUser(LoginUserDTO loginUserDTO)
        {
            var user = await _userRepository.GetUserByUsernameOrEmail(loginUserDTO.UsernameEmail);

            if (user == null)
                throw new UnauthorizedAccessException("Invalid username or password.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginUserDTO.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new UnauthorizedAccessException("Invalid username or password.");

            return await GenerateJwtToken(user);
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            throw new NotImplementedException();
        }
    }
}
