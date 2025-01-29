using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController()
        {
            
        }

        [HttpGet("login")]
        public IActionResult LoginUser()
        {
            throw new NotImplementedException();
        }

        
    }
}
