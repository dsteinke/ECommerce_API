using ECommerce_API.Application;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductAddDTO productAddDTO)
        {
            await _productService.AddProduct(productAddDTO);

            return Ok(productAddDTO);
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();

            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var result = await _productService.GetProductById(productId);

            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct([FromQuery] ProductRequestDTO productDTO)
        {
            var result = await _productService.SearchProducts(productDTO);

            return Ok(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO productUpdateDTO)
        {
            await _productService.UpdateProduct(productUpdateDTO);

            return Ok();
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await _productService.DeleteProduct(productId);

            return Ok();
        }

    }
}
