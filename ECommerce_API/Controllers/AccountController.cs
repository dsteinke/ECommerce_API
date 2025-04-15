using ECommerce.Application.DTO.Identity;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _accountService;
        private readonly IJwtService _jwtService;

        public AccountController(IAuthService accountService, IJwtService jwtService)
        {
            _accountService = accountService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerDTO)
        {
            var result = await _accountService.RegisterUser(registerDTO);

            if (result == null)
                return BadRequest(new { message = "Registration failed. Please check your input data." });

            return Ok(result);
        }

        /// <summary>
        /// Login of existing user
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResponse), 200)]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginDTO)
        {
            var (authenticationResponse, refreshToken) = await _accountService.LoginUser(loginDTO);

            if (authenticationResponse == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
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

        /// <summary>
        /// Signs out user
        /// </summary>
        /// <returns></returns>
        [HttpGet("sign-out")]
        public async Task<IActionResult> LogoutUser()
        {
            await _accountService.LogoutUser();

            Response.Cookies.Delete("refreshToken");

            return Ok(new { message = "Successfully logged out." });
        }

        /// <summary>
        /// Refreshes the access token using the refresh token stored in the cookie.
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized(new { message = "Refresh token is missing." });
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
