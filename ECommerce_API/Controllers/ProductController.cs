using Interfaces;
using Interfaces.DTO.ProductDTO;
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

        [HttpPost("/create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductAddDTO productAddDTO)
        {
            await _productService.AddProduct(productAddDTO);

            return Ok(productAddDTO);
        }

        [HttpGet("/products")]
        public async Task<IActionResult> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProduct()
        {
            throw new NotImplementedException();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> UpdateProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

    }
}
