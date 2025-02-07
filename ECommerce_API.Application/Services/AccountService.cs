using ECommerce_API.Application.DTO.Identity;
using ECommerce_API.Application.Interfaces;
using ECommerce_API.Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerce_API.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly ICartRepository _cartRepository;

        public AccountService
            (SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJwtService jwtService, ICartRepository cartRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
            _cartRepository = cartRepository;
        }

        public async Task<(AuthenticationResponse, string)> LoginUser(LoginDTO loginDTO)
        {
            var result = await _signInManager.PasswordSignInAsync
                (loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(loginDTO.Email);

                var jwtToken = _jwtService.CreateJwtToken(user);
                var refreshToken = _jwtService.CreateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                await _userManager.UpdateAsync(user);

                return (jwtToken, refreshToken);
            }
            return (null, null);
        }

        public async Task LogoutUser()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthenticationResponse> RegisterUser(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");

                user.Cart = await _cartRepository.AddCartToUser(user);

                await _signInManager.SignInAsync(user, isPersistent: false);

                return _jwtService.CreateJwtToken(user);
            }

            return null;
        }
    }
}
