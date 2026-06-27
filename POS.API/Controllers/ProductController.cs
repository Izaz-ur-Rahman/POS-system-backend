using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.DTOs.Product;
using POS.Application.Interfaces;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
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
        public async Task<IActionResult> Create(CreateProductDto dto)
        {
            await _service.Create(dto);
            return Ok("Product created");
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Update(int id, UpdateProductDto dto)
        //{
        //    await _service.Update(id, dto);
        //    return Ok("Product updated");
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateProductDto dto)
        {
            await _service.Update(id, dto);
            return Ok("Product updated");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Product deleted");
        }

        [HttpGet("low-stock")]
public async Task<IActionResult> GetLowStockProducts()
{
    var result =
        await _service.GetLowStockProducts();

    return Ok(result);
}
    }
}