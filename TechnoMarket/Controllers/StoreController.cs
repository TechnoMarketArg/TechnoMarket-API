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
        [Authorize(Roles = "Admin")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var store = _storeService.GetById(id);

            return Ok(store);
        }

        [HttpGet("GetStoreWithProducts")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetStoreWithProducts()
        {
            return Ok(_storeService.GetStoreWithProducts());
        }

        [HttpGet("{storeId}/inventory")]
        public IActionResult StoreAndInventory([FromRoute] Guid storeId)
        {
            return Ok(_storeService.StoreAndInventory(storeId));
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

            _storeService.CreateStore(store, userId);

            return Ok("Store created successfully");
        }

        [HttpPut("{idStore}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update([FromRoute] Guid idStore, [FromBody] StoreUpdateDTO storeDTO)
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

            if (idStore != user.Store.Id)
            {
                return Forbid("No puedes actualizar datos de tiendas que no te pertenecen");
            }

            try
            {
                _storeService.Update(idStore, storeDTO);
                return Ok();

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid storeId)
        {
            try
            {
                _storeService.Delete(storeId);
                return Ok();

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}