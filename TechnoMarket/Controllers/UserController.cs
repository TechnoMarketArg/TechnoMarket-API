using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using TechnoMarket.Application.IServices;
using TechnoMarket.Application.Services;
using TechnoMarket.Domain.DTOs;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Models;

namespace TechnoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IStoreService _storeService;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IStoreService storeService)
        {
            _userService = userService;
            _storeService = storeService;
        }

        [HttpPost("promote-to-admin")]
        [Authorize]
        public IActionResult PromoteToAdmin()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim not found.");
            }

            var userId = Guid.Parse(userIdClaim);

            try
            {
                _userService.PromoteToAdmin(userId);
                return Ok(new { message = "User promoted to admin successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("GetByEmail/{email}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetByEmail([FromRoute] string email)
        {
            return Ok(_userService.GetByEmail(email));
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            var users = _userService.Get();
            return Ok(users);
        }

        [HttpGet("Profile")]
        [Authorize]
        public IActionResult Profile()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim not found.");
            }

            var userId = Guid.Parse(userIdClaim);

            var user = _userService.GetById(userId);

            ProfileDTO profileDTO = new ProfileDTO()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return Ok(profileDTO);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public IActionResult Register([FromBody] UserCreateDTO userDTO)
        {
            var createdUser = _userService.CreateUser(userDTO);
            return Ok(createdUser);
        }

        [HttpPut("UpdateUser/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateUser([FromRoute] string id, [FromBody] UserModel userModel)
        {
            var IdGuid = Guid.Parse(id);
            _userService.UpdateUser(userModel, IdGuid);
            return Ok("User updated successfully");
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Update([FromBody] UserUpdateDTO userUpdateDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim not found.");
            }

            var userId = Guid.Parse(userIdClaim);
            _userService.Update(userUpdateDTO, userId);
            return Ok("User updated successfully");
        }

        [HttpPut("ChangeActive/{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeActive([FromRoute] Guid id)
        {
            _userService.ChangeActive(id);
            return Ok();
        }

        [HttpPut("DeactivateSelf")]
        [Authorize]
        public IActionResult DeactivateSelf()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim not found.");
            }

            var idGuid = Guid.Parse(userIdClaim);

            _userService.ChangeActive(idGuid);
            return Ok();
        }



        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser([FromRoute] Guid id)
        {
            return Ok(_userService.DeleteUser(id));
        }

    }
}