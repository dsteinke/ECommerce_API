using ECommerce_API.Application;
using ECommerce_API.Application.DTO.Identity;
using ECommerce_API.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(" , ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage));

                return BadRequest(errorMessage);
            }
            var result = await _accountService.RegisterUser(registerDTO);

            if (result == null)
            {
                return BadRequest("Registration failed. Please check your input data.");
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(" , ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage));

                return BadRequest(errorMessage);
            }

            var authenticationResponse = await _accountService.LoginUser(loginDTO);

            if (authenticationResponse == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(authenticationResponse);
        }

        [HttpGet("sign-out")]
        public async Task<IActionResult> LogoutUser()
        {
            await _accountService.LogoutUser();

            return Ok("Successfully logged out.");
        }
    }
}
