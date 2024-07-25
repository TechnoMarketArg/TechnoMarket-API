using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechnoMarket.Application.IServices;
using TechnoMarket.Application.Services;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserService _userService;
        private readonly IStoreService _storeService;
        public ProductController(IProductService productService, IUserService userService, IStoreService storeService)
        {
            _productService = productService;
            _userService = userService;
            _storeService = storeService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Product> GetById([FromRoute] Guid id)
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_productService.GetById(id));
        }

        [HttpGet("{productId}")]
        public IActionResult GetProduct(Guid productId) 
        {
            var product = _productService.GetById(productId);

            if (product == null)
            {
                return NotFound();
            }

            ProductGetDTO productGetDTO = new ProductGetDTO()
            {
                Id = productId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Offer = product.Offer,
                Discount = product.Discount,
                Status = product.Status,
                StoreId = product.StoreId,
            };

            return Ok(productGetDTO);
        }

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            return Ok(_productService.GetCategories());
        }

        [HttpGet("category/{categoryId}")]
        public ActionResult<List<ProductGetDTO>> GetProductsByCategory([FromRoute] Guid categoryId)
        {
            var products = _productService.GetProductsByCategory(categoryId);
            if (products == null || !products.Any())
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost("add")]
        [Authorize(Roles = "Seller")]
        public ActionResult<Product> AddProduct([FromBody] ProductCreateDTO product)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim not found.");
            }

            var userId = Guid.Parse(userIdClaim);

            User user = _userService.GetById(userId);

            var storeId = user.Store.Id;
            Store store = _storeService.GetById(storeId);
            if (store == null)
            {
                return BadRequest("Store not found.");
            }

            var newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                StoreId = storeId

            };

            try
            {
                _productService.AddProduct(newProduct);
                return Ok(newProduct);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding product: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public void UpdateProduct(ProductUpdateDTO productDTO, [FromRoute] Guid id)
        {
            try
            {
                _productService.UpdateProduct(productDTO, id);
                Ok("Product Actualizado");
            }catch (Exception ex)
            {
                BadRequest($"Error Update product: {ex.Message}");
            }
            
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Product> DeleteProduct([FromRoute] Guid id)
        {
            return Ok(_productService.DeleteProduct(id));
        }

        
    }
}