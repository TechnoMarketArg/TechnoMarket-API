using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Application.Services;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
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
        public IActionResult AddUser([FromBody]User user)
        {
            return Ok(_userService.AddUser(user));
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromQuery] int id)
        {
            return Ok(_userService.DeleteUser(id));
        }
    }
}
