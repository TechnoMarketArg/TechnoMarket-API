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
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IUserService _userService;
        public StoreController(IStoreService storeService, IUserService userService)
        {
            _storeService = storeService;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetStores()
        {
            return Ok(_storeService.GetStores());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var store = _storeService.GetById(id);
            
            return Ok(store);
        }

        [HttpGet("GetStoreWithProducts")]
        public IActionResult GetStoreWithProducts()
        {
            return Ok(_storeService.GetStoreWithProducts());
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult CreateStore([FromBody] CreateStoreDTO createStoreDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim not found.");
            }

            var userId = Guid.Parse(userIdClaim);

            var user = _userService.GetById(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (user.Role != UserRole.Customer)
            {
                return BadRequest("Only customer can create store");
            }

            var store = new Store
            {
                Name = createStoreDTO.Name,
                Description = createStoreDTO.Description,
                idOwner = user.Id,
            };

            _storeService.CreateStore(store);

            UserUpdateDTO userModel = new UserUpdateDTO
            {
                Email = user.Email,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            _userService.Update(userModel, user.Id, 2);

            return Ok("Store created successfully");
        }
    }
}