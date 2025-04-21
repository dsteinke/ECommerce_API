using AutoMapper;
using ECommerce.Application.Interfaces.Repositories;
using ECommerce.Application.Services;
using ECommerce.Domain.Entities;
using Moq;

namespace ECommerce.Test.Services
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _cartRepositoryMock = new Mock<ICartRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly CartService _cartService;

        public CartServiceTests()
        {
            _cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task AddItemToCart_Should_Add_Item_To_Cart()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };

            _cartRepositoryMock
                .Setup(x => x.AddItemToCart(userId, productId, 2));
                
        }

    }
}
