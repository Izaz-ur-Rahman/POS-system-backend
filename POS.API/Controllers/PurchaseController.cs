using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.DTOs.Purchase;
using POS.Application.Interfaces.Purchase;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _service;

        public PurchaseController(IPurchaseService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseDto dto)
        {
            await _service.CreatePurchase(dto);
            return Ok("Purchase created & stock updated");
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);

            if (result == null)
                return NotFound("Purchase not found");

            return Ok(result);
        }
    }
}
