using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces.Inventory;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _service;

        public InventoryController(
            IInventoryService service)
        {
            _service = service;
        }

        [HttpGet("movement/{productId}")]
        public async Task<IActionResult>
            GetStockMovement(int productId)
        {
            var result =
                await _service.GetStockMovement(productId);

            return Ok(result);
        }

        [HttpGet("valuation")]
        public async Task<IActionResult> GetInventoryValuation()
        {
            var result = await _service.GetInventoryValuation();
            return Ok(result);
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock()
        {
            var result = await _service.GetLowStockProducts();
            return Ok(result);
        }

        [HttpGet("profit")]
        public async Task<IActionResult> GetProfit()
        {
            var result = await _service.GetProfitSummary();
            return Ok(result);
        }
        [HttpGet("profit/daily")]
        public async Task<IActionResult> GetDailyProfit()
        {
            return Ok(await _service.GetDailyProfit());
        }
        [HttpGet("profit/margin")]
        public async Task<IActionResult> GetMargin()
        {
            return Ok(await _service.GetProfitMargin());
        }

        [HttpGet("product-profit")]
        public async Task<IActionResult> GetProductProfit()
        {
            return Ok(await _service.GetProductProfitReport());
        }
        [HttpGet("ledger/{productId}")]
        public async Task<IActionResult> GetLedger(int productId)
        {
            return Ok(await _service.GetStockLedger(productId));
        }
        [HttpGet("reorder-alerts")]
        public async Task<IActionResult> GetReorderAlerts()
        {
            var result = await _service.GetReorderAlerts();
            return Ok(result);
        }
    }
}