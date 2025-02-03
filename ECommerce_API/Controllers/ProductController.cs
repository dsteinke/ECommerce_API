using ECommerce_API.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="productAddDTO"></param>
        /// <returns></returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductAddDTO productAddDTO)
        {
            await _productService.AddProduct(productAddDTO);

            return Ok();
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("products")]
        [ProducesResponseType(typeof(List<ProductResponseDTO>), 200)]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _productService.GetAllProducts();

            return Ok(result);
        }

        /// <summary>
        /// Gets product by ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(ProductResponseDTO), 200)]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var result = await _productService.GetProductById(productId);

            return Ok(result);
        }

        /// <summary>
        /// Searches for products
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns></returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<ProductResponseDTO>), 200)]
        public async Task<IActionResult> SearchProduct([FromQuery] ProductRequestDTO productDTO)
        {
            var result = await _productService.SearchProducts(productDTO);

            return Ok(result);
        }

        /// <summary>
        /// Updates existing product
        /// </summary>
        /// <param name="productUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("edit")]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO productUpdateDTO)
        {
            await _productService.UpdateProduct(productUpdateDTO);

            return Ok();
        }

        /// <summary>
        /// Deletes product by ProductId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await _productService.DeleteProduct(productId);

            return Ok();
        }

    }
}
