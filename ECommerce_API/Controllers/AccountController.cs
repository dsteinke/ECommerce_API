using ECommerce_API.Application;
using ECommerce_API.Application.DTO.Identity;
using ECommerce_API.Application.Interfaces;
using ECommerce_API.Core.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IJwtService _jwtService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(IAccountService accountService, IJwtService jwtService, UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _jwtService = jwtService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerDTO)
        {
            var result = await _accountService.RegisterUser(registerDTO);

            if (result == null)
                return BadRequest("Registration failed. Please check your input data.");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginDTO)
        {
            var (authenticationResponse, refreshToken) = await _accountService.LoginUser(loginDTO);

            if (authenticationResponse == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7) 
            });

            return Ok(authenticationResponse);
        }

        [HttpGet("sign-out")]
        public async Task<IActionResult> LogoutUser()
        {
            await _accountService.LogoutUser();

            Response.Cookies.Delete("refreshToken");

            return Ok("Successfully logged out.");
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized("Refresh token is missing.");
            }

            var (authenticationResponse, newRefreshToken) = await _jwtService.GetRefreshToken(refreshToken);

            Response.Cookies.Append("refreshToken", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(authenticationResponse);
        }
    }
}
