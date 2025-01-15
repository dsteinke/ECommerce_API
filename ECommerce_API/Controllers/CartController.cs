using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {


        public CartController()
        {
            
        }

        public async Task<IActionResult> GetCart()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> AddToCart()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> RemoveFromCart()
        {
            throw new NotImplementedException();
        }
    }
}
