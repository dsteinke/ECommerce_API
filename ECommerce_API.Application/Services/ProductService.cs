using AutoMapper;
using ECommerce.Application.DTO.Product;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Interfaces.Services;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public ProductService
            (IProductRepository productRepository, IImageService imageService, IMapper mapper)
        {
            _productRepository = productRepository;
            _imageService = imageService;
            _mapper = mapper;
        }

        public async Task AddProduct(ProductAddDTO productAddDTO)
        {
            var product = productAddDTO.ToProduct();

            if (productAddDTO.Images != null && productAddDTO.Images.Any())
            {
                product.ProductImages = new List<ProductImage>();
                var imageUrls = await _imageService.SaveImagesToStorage(productAddDTO.Images);

                foreach (var imageUrl in imageUrls)
                {
                    product.ProductImages.Add(new ProductImage
                    {
                        ImageUrl = imageUrl,
                        ProductId = product.ProductId,
                    });
                }
            }

            await _productRepository.AddProduct(product);
        }

        public async Task DeleteProduct(Guid productId)
        {
            var productToDelete = await _productRepository.GetProductById(productId);

            if (productToDelete == null)
                throw new KeyNotFoundException($"Product with Id: {productId} does not exist");

            if (productToDelete.ProductImages != null && productToDelete.ProductImages.Any())
            {
                var imageUrls = productToDelete.ProductImages
                    .Select(x => x.ImageUrl)
                    .ToList();

                await _imageService.DeleteImagesFromStorage(imageUrls);
            }

            await _productRepository.DeleteProduct(productId);
        }

        public async Task<List<ProductResponseDTO>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();

            var response = _mapper.Map<List<ProductResponseDTO>>(products);

            return response;
        }

        public async Task<ProductResponseDTO> GetProductById(Guid productId)
        {
            var product = await _productRepository.GetProductById(productId);

            if (product == null)
                throw new KeyNotFoundException($"ProductId: '{productId}' does not exist");

            var response = _mapper.Map<ProductResponseDTO>(product);

            return response;
        }

        public async Task<List<ProductResponseDTO>> SearchProducts(ProductRequestDTO productDTO)
        {
            var searchedProducts = await _productRepository.SearchProduct(productDTO);

            if (!searchedProducts.Any())
                return new List<ProductResponseDTO>();

            return _mapper.Map<List<ProductResponseDTO>>(searchedProducts);
        }

        public async Task UpdateProduct(ProductUpdateDTO productUpdateDTO)
        {
            var productToUpdate = await _productRepository.GetProductById(productUpdateDTO.ProductId);

            if (productToUpdate == null)
                throw new KeyNotFoundException
                    ($"Given ProductId: {productUpdateDTO.ProductId} does not exist");

            _mapper.Map(productUpdateDTO, productToUpdate);

            await _productRepository.UpdateProduct(productToUpdate);
        }
    }
}
