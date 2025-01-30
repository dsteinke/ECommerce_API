using ECommerce_API.Application.DTO.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        public AccountController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join(" , ", ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(e => e.ErrorMessage));

                return Problem(errorMessage);
            }

        }


    }
}
