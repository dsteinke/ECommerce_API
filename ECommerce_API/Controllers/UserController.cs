using Interfaces;
using Interfaces.DTO.User;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserAddDTO userAddDTO)
        {
            await _userService.RegisterUser(userAddDTO);

            return Ok();
        }
    }
}
