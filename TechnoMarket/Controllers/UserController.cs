using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get([FromRoute]string email)
        {
            return Ok(_userService.GetByEmail(email));
        }

        [HttpGet("All")]
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
        public IActionResult DeleteUser([FromQuery] Guid id)
        {
            return Ok(_userService.DeleteUser(id));
        }

        [HttpPut]
        public IActionResult UpdateEmail(string email)
        {
            Guid userId = Guid.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty);//NameIdentifier = sub = Id
            var user = _userService.GetById(userId);
            UserModel userModel = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password, // Asegúrate de manejar las contraseñas de manera segura
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role
            };
            _userService.Update(userModel);
            return Ok();
        }

        [HttpPost("update")]
        [Authorize]
        public IActionResult Update([FromBody] UserModel userModel)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return BadRequest("User ID claim not found.");
            }

            var userId = Guid.Parse(userIdClaim);
            userModel.Id = userId;
            _userService.Update(userModel);
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


    }
}
