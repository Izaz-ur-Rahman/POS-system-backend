using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces.Reports;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("daily-sales")]
        public async Task<IActionResult> GetDailySales([FromQuery] DateTime date)
        {
            var result = await _service.GetDailySalesReport(date);
            return Ok(result);
        }

        [HttpGet("monthly-revenue")]
        public async Task<IActionResult> GetMonthlyRevenue([FromQuery] int year)
        {
            var result = await _service.GetMonthlyRevenue(year);
            return Ok(result);
        }
    }
}