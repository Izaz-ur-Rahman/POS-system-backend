using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.DTOs.Customer;
using POS.Application.Interfaces;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
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
        public async Task<IActionResult> Create(CreateCustomerDto dto)
        {
            await _service.Create(dto);
            return Ok("Customer created");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCustomerDto dto)
        {
            await _service.Update(id, dto);
            return Ok("Customer updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Customer deleted");
        }
    }
}