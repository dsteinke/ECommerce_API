using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ECommerce.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly string _imagePath;
        private readonly string _baseUrl;
        private const long _maxFileSize = 5 * 1024 * 1024;
        private readonly List<string> _allowedExtensions
            = new List<string> { ".jpg", ".jpeg", ".png" };

        private readonly IProductRepository _productRepository;

        public ImageService(IConfiguration config, IProductRepository productRepository)
        {
            _imagePath = config["ImageUpload:ImagePath"];
            _baseUrl = config["ImageUpload:BaseUrl"];
            _productRepository = productRepository;
        }

        private void ValidateFileSizeAndExtension(IFormFile file)
        {
            if (file.Length > _maxFileSize)
                throw new ArgumentException($"File '{file.FileName}' exceeds the max allowed size of 5 MB");

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!_allowedExtensions.Contains(extension))
                throw new ArgumentException($"File '{file.FileName}' has an invalid file extension.");
        }

        public async Task<List<string>> SaveImagesToStorage(List<IFormFile> images)
        {
            var urls = new List<string>();

            foreach (var image in images)
            {
                ValidateFileSizeAndExtension(image);

                var imageName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                var fullPath = Path.Combine(_imagePath, imageName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await image.CopyToAsync(stream);

                urls.Add($"{_baseUrl}/{imageName}");
            }

            return urls;
        }

        private async Task DeleteImage(string imageUrl)
        {
            var imageName = Path.GetFileName(imageUrl);
            var path = Path.Combine(_imagePath, imageName);

            if (File.Exists(path))
                await Task.Run(() => File.Delete(path));
        }

        public async Task DeleteImagesFromStorage(List<string> imageUrls)
        {
            var deleteTasks = imageUrls.Select(imageUrl => DeleteImage(imageUrl));

            await Task.WhenAll(deleteTasks);
        }

        public async Task<List<string?>> GetImages(Guid productId)
        {
            var product = await _productRepository.GetProductById(productId);

            if (product == null)
                throw new KeyNotFoundException($"ProductId: '{productId}' does not exist");

            var imageUrls = product.ProductImages
                .Select(x => x.ImageUrl)
                .ToList();

            return imageUrls;
        }

        public async Task<bool> AddProductImages(Guid productId, List<IFormFile> images)
        {
            var product = await _productRepository.GetProductById(productId);

            if (product == null)
                throw new KeyNotFoundException($"ProductId: '{productId}' does not exist");

            var imageUrls = await SaveImagesToStorage(images);

            if (product.ProductImages == null)
                product.ProductImages = new List<ProductImage>();

            foreach (var imageUrl in imageUrls)
            {
                product.ProductImages.Add(new ProductImage { ImageUrl = imageUrl });
            }

            await _productRepository.AddProductImages(product);

            return true;
        }

        public async Task<bool> RemoveProductImages(Guid productId, List<string> imageUrls)
        {
            var product = await _productRepository.GetProductById(productId);

            if (product == null)
                throw new KeyNotFoundException($"ProductId: '{productId}' does not exist");

            if (product.ProductImages == null || !product.ProductImages.Any())
                return false;

            var images = product.ProductImages
                .Where(x => imageUrls.Contains(x.ImageUrl))
                .ToList();

            if (!images.Any())
                return false;

            var imagesToDelete = images
                .Select(x => x.ImageUrl)
                .ToList();

            await DeleteImagesFromStorage(imagesToDelete);

            await _productRepository.RemoveProductImages(product);

            return true;
        }
    }
}
