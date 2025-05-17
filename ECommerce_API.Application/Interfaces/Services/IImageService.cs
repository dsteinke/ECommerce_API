using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IImageService
    {
        Task<List<string>>? SaveImagesToStorage(List<IFormFile> images);
        Task DeleteImagesFromStorage(List<string> imageUrls);
        Task<List<string?>> GetImages(Guid productId);
        Task<bool> AddProductImages(Guid productId, List<IFormFile> images);
        Task<bool> RemoveProductImages(Guid productId, List<string> imageUrls);
    }
}
