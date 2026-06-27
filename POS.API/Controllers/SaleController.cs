using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.DTOs.Sale;
using POS.Application.Interfaces.Sale;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _service;

        public SaleController(ISaleService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleDto dto)
        {
            //await _service.CreateSale(dto);
            //return Ok("Sale completed & stock updated");
            try
            {
                await _service.CreateSale(dto);
                return Ok("Sale completed & stock updated");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sale = await _service.GetById(id);

            if (sale == null)
                return NotFound("Sale not found");

            return Ok(sale);
        }
    }
}