using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> CreateProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> SearchProduct()
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> UpdateProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

    }
}
