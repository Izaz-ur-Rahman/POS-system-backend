using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces.Stock;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StockController : ControllerBase
    {
        private readonly IStockService _service;

        public StockController(IStockService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllStock());
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> LowStock()
        {
            return Ok(await _service.GetLowStock());
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProduct(int productId)
        {
            var result = await _service.GetProductStock(productId);

            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}