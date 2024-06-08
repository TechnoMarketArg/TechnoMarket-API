using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechnoMarket.Application.IServices;
using TechnoMarket.Application.Services;
using TechnoMarket.Domain.Entities;

namespace TechnoMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public IActionResult GetStores() 
        { 
            return Ok(_storeService.GetStores()); 
        }

        [HttpGet("GetStoreWithProducts")]
        public IActionResult GetStoreWithProducts()
        {
            return Ok(_storeService.GetStoreWithProducts());
        }
    }
}
