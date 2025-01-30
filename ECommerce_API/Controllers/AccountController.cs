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

        [HttpPost]
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

            if (!result.Succeeded)
            {
                var errorMessages = string.Join(" , ", result.Errors.Select(e => e.Description));
                return BadRequest(errorMessages);
            }

            return Ok("User successfully created.");
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(" , ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage));

                return BadRequest(errorMessage);
            }

            var user = await _accountService.LoginUser(loginDTO);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok("Successfully logged in.");
        }

        [HttpGet]
        public async Task<IActionResult> LogoutUser()
        {
            await _accountService.LogoutUser();

            return Ok();
        }

    }
}
