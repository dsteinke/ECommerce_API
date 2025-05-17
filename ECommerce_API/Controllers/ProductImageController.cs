using ECommerce.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/v1/images")]
    [ApiController]
    public class ProductImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ProductImageController
            (IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductImages([FromRoute] Guid productId)
        {
            var result = await _imageService.GetImages(productId);

            return Ok(result);
        }

        [HttpPost("add/{productId}")]
        public async Task<IActionResult> AddProductImages
            ([FromRoute] Guid productId, [FromForm] List<IFormFile> images)
        {
            if (images == null || !images.Any())
                return BadRequest(new { message = "No images were provided." });

            await _imageService.AddProductImages(productId, images);

            return Ok(new { message = "Images added successfully." });
        }

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveProductImages
            ([FromRoute] Guid productId, [FromBody] List<string> imageUrls)
        {
            var result = await _imageService.RemoveProductImages(productId, imageUrls);

            if (!result)
                return NotFound(new { message = "No matching images found to remove." });

            return Ok(new { message = "Selected images removed successfully." });
        }
    }
}
