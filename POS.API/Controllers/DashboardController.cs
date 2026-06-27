using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Application.Interfaces.Dashboard;
using POS.Domain.Entities;
using POS.Infrastructure.Data;
namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;
        private readonly AppDbContext _context;

        public DashboardController(
            IDashboardService service,
            AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet("inventory-summary")]
        public async Task<IActionResult>
            GetInventorySummary()
        {
            var result =
                await _service.GetInventorySummary();

            return Ok(result);
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            return Ok(await _service.GetSummary());
        }

        [HttpGet("top-selling-products")]
        public async Task<IActionResult>
    GetTopSellingProducts(
        [FromQuery] int top = 10)
        {
            return Ok(
                await _service
                    .GetTopSellingProducts(top));
        }

        [HttpGet("sales-trend")]
        public async Task<IActionResult>
    GetSalesTrend(
        [FromQuery] int days = 30)
        {
            return Ok(
                await _service.GetSalesTrend(days));
        }

        [HttpGet("profit")]
        public async Task<IActionResult> GetProfit()
        {
            return Ok(await _service.GetProfit());
        }
        [HttpPost("reset-test-data")]
        public async Task<IActionResult> ResetTestData()
        {
            // 🔥 remove sale details first (child table)
            _context.SaleItems.RemoveRange(_context.SaleItems);

            // 🔥 remove sales
            _context.Sales.RemoveRange(_context.Sales);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Test data reset successfully. Dashboard is now clean."
            });
        }

    }
}