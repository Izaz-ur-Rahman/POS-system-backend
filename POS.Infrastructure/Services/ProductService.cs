using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Product;
using POS.Application.Interfaces;
using POS.Domain.Entities;
using POS.Infrastructure.Data;

namespace POS.Infrastructure.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDto>> GetAll()
        {
            return await _context.Products
                .Include(x => x.Category)
                .Select(x => new ProductDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Barcode = x.Barcode,
                    PurchasePrice = x.PurchasePrice,
                    SalePrice = x.SalePrice,
                    StockQuantity = x.StockQuantity,
                    ImagePath = x.ImagePath,
                    IsActive = x.IsActive,
                    CategoryName = x.Category!.Name
                })
                .ToListAsync();
        }

        public async Task<ProductDto?> GetById(int id)
        {
            var product = await _context.Products
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Barcode = product.Barcode,
                PurchasePrice = product.PurchasePrice,
                SalePrice = product.SalePrice,
                StockQuantity = product.StockQuantity,
                ImagePath = product.ImagePath,
                IsActive = product.IsActive,
                CategoryName = product.Category!.Name
            };
        }

        public async Task Create(CreateProductDto dto)
        {
            var imagePath = await SaveImage(dto.Image);

            var product = new Product
            {
                Name = dto.Name,
                Barcode = dto.Barcode,
                PurchasePrice = dto.PurchasePrice,
                SalePrice = dto.SalePrice,
                StockQuantity = dto.StockQuantity,
                CategoryId = dto.CategoryId,
                ImagePath = imagePath
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, UpdateProductDto dto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return;

            product.Name = dto.Name;
            product.Barcode = dto.Barcode;
            product.PurchasePrice = dto.PurchasePrice;
            product.SalePrice = dto.SalePrice;
            product.StockQuantity = dto.StockQuantity;
            product.CategoryId = dto.CategoryId;
            product.IsActive = dto.IsActive;

            // ✅ IMAGE UPDATE LOGIC (IMPORTANT FIX)
            if (dto.Image != null)
            {
                product.ImagePath = await SaveImage(dto.Image);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        private async Task<string?> SaveImage(IFormFile? image)
        {
            if (image == null)
                return null;

            var folder = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot/uploads/products");

            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid() + Path.GetExtension(image.FileName);

            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return $"/uploads/products/{fileName}";
        }

        public async Task<List<LowStockProductDto>>
    GetLowStockProducts()
        {
            return await _context.Products
                .Where(x =>
                    x.StockQuantity <= x.MinStockLevel)
                .Select(x => new LowStockProductDto
                {
                    ProductId = x.Id,
                    ProductName = x.Name,
                    StockQuantity = x.StockQuantity,
                    MinStockLevel = x.MinStockLevel
                })
                .OrderBy(x => x.StockQuantity)
                .ToListAsync();
        }
    }
}