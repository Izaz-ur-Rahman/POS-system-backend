using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.DTOs.Supplier;
using POS.Application.Interfaces;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService service)
        {
            _service = service;
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
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSupplierDto dto)
        {
            await _service.Create(dto);
            return Ok("Supplier created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSupplierDto dto)
        {
            await _service.Update(id, dto);
            return Ok("Supplier updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Supplier deleted");
        }
    }
}