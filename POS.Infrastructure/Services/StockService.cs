using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Stock;
using POS.Application.Interfaces.Stock;
using POS.Infrastructure.Data;

namespace POS.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly AppDbContext _context;

        public StockService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductStockDto>> GetAllStock()
        {
            return await _context.Products
                .Select(p => new ProductStockDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    StockQuantity = p.StockQuantity,
                    MinStockLevel = p.MinStockLevel,
                    IsLowStock = p.StockQuantity <= p.MinStockLevel
                })
                .ToListAsync();
        }

        public async Task<List<ProductStockDto>> GetLowStock()
        {
            return await _context.Products
                .Where(p => p.StockQuantity <= p.MinStockLevel)
                .Select(p => new ProductStockDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    StockQuantity = p.StockQuantity,
                    MinStockLevel = p.MinStockLevel,
                    IsLowStock = true
                })
                .ToListAsync();
        }

        public async Task<ProductStockDto?> GetProductStock(int productId)
        {
            return await _context.Products
                .Where(p => p.Id == productId)
                .Select(p => new ProductStockDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    StockQuantity = p.StockQuantity,
                    MinStockLevel = p.MinStockLevel,
                    IsLowStock = p.StockQuantity <= p.MinStockLevel
                })
                .FirstOrDefaultAsync();
        }
    }
}