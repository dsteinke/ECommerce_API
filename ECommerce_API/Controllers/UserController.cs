using ECommerce_API.Application;
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserByUserId([FromRoute] Guid userId)
        {
            var user = await _userService.GetUserById(userId);

            return Ok(user);
        }
    }
}
