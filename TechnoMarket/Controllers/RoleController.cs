using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Application.IServices;
using TechnoMarket.Application.Services;

namespace TechnoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_roleService.Get());
        }
    }
}
