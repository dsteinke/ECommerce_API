using AutoMapper;
using ECommerce_API.Application;
using ECommerce_API.Application.Interfaces.Repositories;
using ECommerce_API.Core;
using FluentAssertions;
using Moq;

namespace ECommerce_API.Test.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddProduct_Should_Call_Repository_And_Return_Mapped_Product()
        {
            // Arrange
            var productAddDTO = new ProductAddDTO { Name = "TestProduct", Price = 100 };
            var product = new Product { ProductId = Guid.NewGuid(), Name = "TestProduct", Price = 100 };
            var responseDTO = new ProductResponseDTO { Name = "TestProduct", Price = 100 };

            _productRepositoryMock
                .Setup(r => r.AddProduct(It.IsAny<Product>()))
                .ReturnsAsync(product);

            _mapperMock
                .Setup(m => m.Map<ProductResponseDTO>(It.IsAny<Product>()))
                .Returns(responseDTO);

            // Act
            var result = await _productService.AddProduct(productAddDTO);

            // Assert
            result.Should().BeEquivalentTo(responseDTO);
        }

        [Fact]
        public async Task DeleteProduct_Should_Return_True_When_Product_Exists()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { ProductId = productId, Name = "TestProduct", Price = 100 };

            _productRepositoryMock
                .Setup(r => r.GetProductById(productId))
                .ReturnsAsync(product);

            _productRepositoryMock
                .Setup(r => r.DeleteProduct(productId))
                .ReturnsAsync(true);

            // Act
            var result = await _productService.DeleteProduct(productId);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task DeleteProduct_Should_Throw_KeyNotFoundException_When_Product_Does_Not_Exist()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _productRepositoryMock
                .Setup(x => x.GetProductById(productId))
                .ReturnsAsync((Product)null); // Produkt existiert nicht

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.DeleteProduct(productId));
        }

        [Fact]
        public async Task GetAllProducts_Should_Return_ProductList()
        {
            // Arrange
            var product1 = new Product { ProductId = Guid.NewGuid(), Name = "Product1", Price = 100 };
            var product2 = new Product { ProductId = Guid.NewGuid(), Name = "Product2", Price = 200 };
            var productList = new List<Product> { product1, product2 };

            var responseDTO1 = new ProductResponseDTO { Name = "Product1", Price = 100 };
            var responseDTO2 = new ProductResponseDTO { Name = "Product2", Price = 200 };
            var responseDTOList = new List<ProductResponseDTO> { responseDTO1, responseDTO2 };

            _productRepositoryMock
                .Setup(x => x.GetAllProducts())
                .ReturnsAsync(productList);

            _mapperMock
                .Setup(x => x.Map<List<ProductResponseDTO>>(productList))
                .Returns(responseDTOList);

            // Act
            var result = await _productService.GetAllProducts();

            // Assert
            result.Should().BeEquivalentTo(responseDTOList);
        }

        [Fact]
        public async Task GetProductById_Should_Return_ProductResponse()
        {
            // Arrange
            var product = new Product { ProductId = Guid.NewGuid(), Name = "TestProduct", Price = 100 };

            var responseDTO = new ProductResponseDTO { Name = "TestProduct", Price = 100 };

            _productRepositoryMock
                .Setup(x => x.GetProductById(product.ProductId))
                .ReturnsAsync(product);

            _mapperMock
                .Setup(x => x.Map<ProductResponseDTO>(product))
                .Returns(responseDTO);

            // Act
            var result = await _productService.GetProductById(product.ProductId);

            // Assert
            result.Should().BeEquivalentTo(responseDTO);
        }

        [Fact]
        public async Task GetProductById_Should_Throw_KeyNotFoundException_When_Product_Does_Not_Exist()
        {
            // Arrange
            var productId = Guid.NewGuid();

            _productRepositoryMock
                .Setup(x => x.GetProductById(productId))
                .ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.GetProductById(productId));
        }

        [Fact]
        public async Task SearchProduct_Should_Return_ProductList_When_Products_Exists()
        {
            // Arrange
            var productRequestDTO = new ProductRequestDTO { Name = "TestProduct" };

            var productList = new List<Product>
            {
                new Product { ProductId = Guid.NewGuid(), Name = "TestProduct", Price = 100 },
                new Product { ProductId = Guid.NewGuid(), Name = "TestProduct2", Price = 200 },
            };

            var responseDTOList = new List<ProductResponseDTO>
            {
                new ProductResponseDTO{Name= "TestProduct", Price = 100},
                new ProductResponseDTO{Name= "TestProduct2", Price = 200}
            };

            _productRepositoryMock
                .Setup(x => x.SearchProduct(productRequestDTO))
                .ReturnsAsync(productList);

            _mapperMock
                .Setup(x => x.Map<List<ProductResponseDTO>>(productList))
                .Returns(responseDTOList);

            // Act
            var result = await _productService.SearchProducts(productRequestDTO);

            // Assert
            result.Should().BeEquivalentTo(responseDTOList);
        }

        [Fact]
        public async Task SearchProduct_Should_Return_Empty_List_When_No_Products_Found()
        {
            // Arrange
            var productRequestDTO = new ProductRequestDTO { Name = "NotExistingProduct" };

            _productRepositoryMock
                .Setup(x => x.SearchProduct(productRequestDTO))
                .ReturnsAsync(new List<Product>());

            // Act
            var result = await _productService.SearchProducts(productRequestDTO);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task UpdateProduct_Should_Successfully_Update()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productUpdateDTO = new ProductUpdateDTO { ProductId = productId, Name = "UpdatedName", Price = 200 };
            var existingProduct = new Product { ProductId = productId, Name = "OldName", Price = 100 };
            var responseDTO = new ProductResponseDTO { ProductId = productId, Name = "UpdatedName", Price = 200 };

            _productRepositoryMock
                .Setup(x => x.GetProductById(productId))
                .ReturnsAsync(existingProduct);

            _productRepositoryMock
                .Setup(x => x.UpdateProduct(existingProduct))
                .ReturnsAsync(existingProduct);

            _mapperMock
                .Setup(x => x.Map<ProductResponseDTO>(existingProduct))
                .Returns(responseDTO);

            // Act
            var result = await _productService.UpdateProduct(productUpdateDTO);

            // Assert
            result.Should().BeEquivalentTo(responseDTO);

        }

        [Fact]
        public async Task UpdateProduct_Should_Throw_KeyNotFoundException_When_Product_Does_Not_Exist()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var productUpdateDTO =
                new ProductUpdateDTO { ProductId = productId, Name = "UpdatedName", Price = 200 };

            _productRepositoryMock
                .Setup(x => x.GetProductById(productId))
                .ReturnsAsync((Product)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _productService.UpdateProduct(productUpdateDTO));
        }
    }
}