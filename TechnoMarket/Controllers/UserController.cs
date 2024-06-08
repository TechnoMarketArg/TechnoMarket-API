using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Application.IServices;
using TechnoMarket.Application.Services;
using TechnoMarket.Domain.Entities;
using TechnoMarket.Models;

namespace TechnoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{name}")]
        public IActionResult Get([FromRoute]string name)
        {
            return Ok(_userService.Get(name));
        }

        [HttpGet("All")]
        public IActionResult Get()
        {
            return Ok(_userService.Get());
        }

        [HttpPost]
        public IActionResult AddUser([FromBody]UserDto userDto)
        {
            var user = new User
            {
                FirstName = userDto.FirstName,
                Email = userDto.Email,
                //RoleId = userDto.RoleId,
            };

            return Ok(_userService.AddUser(user));
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromQuery] Guid id)
        {
            return Ok(_userService.DeleteUser(id));
        }
    }
}
