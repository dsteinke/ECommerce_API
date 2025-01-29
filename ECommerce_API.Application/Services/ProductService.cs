using AutoMapper;

namespace ECommerce_API.Application
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductResponseDTO> AddProduct(ProductAddDTO? productAddDTO)
        {
            if (productAddDTO == null)
                throw new ArgumentNullException(nameof(productAddDTO));

            var product = productAddDTO.ToProduct();

            product.ProductId = Guid.NewGuid();

            await _productRepository.AddProduct(product);

            var response = _mapper.Map<ProductResponseDTO>(product);

            return response;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var personToDelete = await _productRepository.GetProductById(productId);

            if (personToDelete == null)
                throw new KeyNotFoundException($"Product with Id: {productId} does not exist");

            await _productRepository.DeleteProduct(productId);

            return true;
        }

        public async Task<List<ProductResponseDTO>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();

            var response = new List<ProductResponseDTO>();

            foreach (var product in products)
            {
                response.Add(_mapper.Map<ProductResponseDTO>(product));
            }

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

        public async Task<ProductResponseDTO> UpdateProduct(ProductUpdateDTO? productUpdateDTO)
        {
            var productToUpdate = await _productRepository.GetProductById(productUpdateDTO.ProductId);

            if (productToUpdate == null)
                throw new ArgumentNullException
                    ($"Given ProductId: {productUpdateDTO.ProductId} does not exist", nameof(productToUpdate));

            productToUpdate.Name = productUpdateDTO.Name;
            productToUpdate.Description = productUpdateDTO.Description;
            productToUpdate.Price = productUpdateDTO.Price;
            productToUpdate.Category = productUpdateDTO.Category;

            await _productRepository.UpdateProduct(productToUpdate);

            var response = _mapper.Map<ProductResponseDTO>(productToUpdate);

            return response;
        }
    }
}
