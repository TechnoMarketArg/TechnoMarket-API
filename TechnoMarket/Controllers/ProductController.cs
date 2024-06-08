using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Application.IServices;
using TechnoMarket.Application.Services;

namespace TechnoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }

        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery]Guid id) 
        {
            var product = _productService.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(_productService.GetById(id));
        }
    }
}
