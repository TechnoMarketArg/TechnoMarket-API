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


        [HttpGet("{email}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get([FromRoute]string email)
        {
            return Ok(_userService.GetByEmail(email));
        }

        [HttpGet("All")]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserCreateDTO userDTO)
        {
            var createdUser = _userService.CreateUser(userDTO);
            return Ok(createdUser);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser([FromQuery] Guid id)
        {
            return Ok(_userService.DeleteUser(id));
        }


        [HttpPost("update")]
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

        [HttpPost("verify-password")]
        [Authorize]
        public IActionResult VerifyPassword([FromBody] PasswordVerificationDTO dto)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim not found.");
            }

            var userId = Guid.Parse(userIdClaim);
            var isValid = _userService.VerifyPassword(userId, dto.Password);
            return Ok(isValid);
        }

        [HttpPut("ChangeActive")]
        [Authorize(Roles = "Admin")]
        public IActionResult ChangeActive([FromQuery] string id)
        {
            var idGuid = Guid.Parse(id);

            _userService.ChangeActive(idGuid);
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

    }
}
