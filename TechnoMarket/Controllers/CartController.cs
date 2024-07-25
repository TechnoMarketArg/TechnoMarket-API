using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechnoMarket.Application.IServices;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;

        public CartController(ICartService cartService, IUserService userService, IProductService productService)
        {
            _cartService = cartService;
            _userService = userService;
            _productService = productService;
        }

        /*[HttpGet("Cart")]
        [Authorize]
        public IActionResult GetCart()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID claim not found.");
            }
            var userIdGuid = Guid.Parse(userId);

            var cart = _cartService.GetCartByUserId(userIdGuid);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        [HttpPost("add")]
        [Authorize]
        public IActionResult AddCartItem([FromBody] CartItemCreateDTO cartItemDto)
        {
            try
            {
                _cartService.AddCartItem(cartItemDto);
                return Ok("Item added to cart.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding item to cart: {ex.Message}");
            }
        }*/

    }
}
