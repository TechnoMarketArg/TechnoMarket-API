using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoMarket.Application.IServices;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Domain.Interfaces;

namespace TechnoMarket.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        /*public CartDTO GetCartByUserId(Guid userId)
        {
            var cart = _cartRepository.GetCartByUserId(userId);
            if (cart == null) return null;

            var cartDto = new CartDTO
            {
                Id = cart.Id,
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice,
                Items = cart.Items.Select(item => new CartItemDTO
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList()
            };

            return cartDto;
        }

        public void AddCartItem(CartItemCreateDTO createDto)
        {
            var product = _productRepository.GetById(createDto.ProductId);
            if (product == null) throw new Exception("Product not found.");

            var newItem = new CartItem
            {
                ProductId = createDto.ProductId,
                Quantity = createDto.Quantity,
                UnitPrice = product.Price
            };

            _cartRepository.Add(newItem);
        }

        public bool RemoveItemFromCart(Guid userId, Guid cartItemId)
        {
            var cart = _cartRepository.GetCartByUserId(userId);
            if (cart == null) return false;

            var cartItem = cart.Items.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null) return false;

            cart.Items.Remove(cartItem);
            cart.TotalPrice -= cartItem.Quantity * cartItem.UnitPrice;

            _cartRepository.Update(cart);
            return true;
        }*/
    }
}
