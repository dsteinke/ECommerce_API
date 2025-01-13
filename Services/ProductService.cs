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
            if(productAddDTO == null)
                throw new ArgumentNullException(nameof(productAddDTO));

            var product = productAddDTO.ToProduct();

            product.ProductId = Guid.NewGuid();

            await _productRepository.AddProduct(product);

            var response = _mapper.Map<ProductDTO>(product);

            return response;
        }

        public Task<bool> DeleteProduct(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetProductById(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductDTO>> SearchProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> UpdateProduct(ProductUpdateDTO? productUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
