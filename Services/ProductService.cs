using AutoMapper;
using Interfaces;
using Interfaces.DTO.ProductDTO;
using RepositoryInterfaces;

namespace Services
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

        public async Task<ProductDTO> AddProduct(ProductAddDTO? productAddDTO)
        {
            if (productAddDTO == null)
                throw new ArgumentNullException(nameof(productAddDTO));

            var product = productAddDTO.ToProduct();

            product.ProductId = Guid.NewGuid();

            await _productRepository.AddProduct(product);

            var response = _mapper.Map<ProductDTO>(product);

            return response;
        }

        public async Task<bool> DeleteProduct(Guid productId)
        {
            var personToDelete = await _productRepository.GetProductById(productId);

            if (personToDelete == null)
                return false;

            await _productRepository.DeleteProduct(productId);

            return true;
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();

            var response = new List<ProductDTO>();

            foreach (var product in products)
            {
                response.Add(_mapper.Map<ProductDTO>(product));
            }

            return response;
        }

        public async Task<ProductDTO> GetProductById(Guid productId)
        {
            var product = await _productRepository.GetProductById(productId);

            if (product == null)
                throw new ArgumentNullException($"ProductId: '{productId}' does not exist");

            var response = _mapper.Map<ProductDTO>(product);

            return response;
        }

        public async Task<List<ProductDTO>> SearchProducts(ProductDTO productDTO)
        {
            var searchedProducts = await _productRepository.SearchProduct(productDTO);

            if (!searchedProducts.Any())
                return new List<ProductDTO>();

            return _mapper.Map<List<ProductDTO>>(searchedProducts);
        }

        public async Task<ProductDTO> UpdateProduct(ProductUpdateDTO? productUpdateDTO)
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

            var response = _mapper.Map<ProductDTO>(productToUpdate);

            return response;
        }
    }
}
