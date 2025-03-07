﻿using ECommerce_API.Application;
using ECommerce_API.Core;
using ECommerce_API.Core.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Infrastructure
{
    public class CartRepository : ICartRepository
    {
        private readonly ECommerceDbContext _context;

        public CartRepository(ECommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> AddCartToUser(ApplicationUser user)
        {
            var cart = new Cart { UserId = user.Id };
            _context.Carts.Add(cart);

            await _context.SaveChangesAsync();

            return cart;
        }

        public async Task AddItemToCart(Guid userId, Guid productId, int quantity)
        {
            var cart = await GetCartByUserId(userId);

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == productId);

            if (existingItem != null)
                existingItem.Quantity += quantity;

            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = quantity,
                };

                cart.CartItems.Add(newItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByUserId(Guid userId)
        {
            var cart = await _context.Carts
                .Include(x => x.CartItems)
                .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (cart == null)
                throw new KeyNotFoundException($"No cart found for user with Id: {userId}.");

            return cart;
        }

        public async Task RemoveItemFromCart(Guid userId, Guid productId)
        {
            var cart = await GetCartByUserId(userId);

            var itemToRemove = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);

            if (itemToRemove == null)
                throw new KeyNotFoundException($"No product with Id: {productId} found in the cart for user with Id: {userId}.");

            _context.CartItems.Remove(itemToRemove);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateCartItemQuantity(Guid userId, Guid productId, int quantity)
        {
            var cart = await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (cart == null)
                throw new KeyNotFoundException($"No user found with Id: {userId}");

            var itemToUpdate = cart.CartItems
                .FirstOrDefault(x => x.ProductId == productId);

            if (itemToUpdate == null)
                throw new KeyNotFoundException($"No product with Id: {productId} found in the cart for user with Id: {userId}.");

            itemToUpdate.Quantity = quantity;

            await _context.SaveChangesAsync();
        }

        public async Task ClearCart(Guid userId)
        {
            var cart = await _context.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => x.UserId == userId);

            _context.CartItems.RemoveRange(cart.CartItems);
        }
    }
}
