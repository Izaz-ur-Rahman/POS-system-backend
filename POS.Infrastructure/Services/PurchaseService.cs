//using POS.Application.DTOs.Purchase;
//using POS.Application.Interfaces.Purchase;
//using POS.Domain.Entities;
//using POS.Infrastructure.Data;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;

//namespace POS.Infrastructure.Services
//{
//    public class PurchaseService : IPurchaseService
//    {
//        private readonly AppDbContext _context;

//        public PurchaseService(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task CreatePurchase(CreatePurchaseDto dto)
//        {
//            var purchase = new Purchase
//            {
//                SupplierId = dto.SupplierId,
//                PurchaseDate = DateTime.UtcNow,
//                Items = new List<PurchaseItem>()
//            };

//            decimal total = 0;

//            foreach (var item in dto.Items)
//            {
//                var product = await _context.Products.FindAsync(item.ProductId);

//                if (product == null)
//                    throw new Exception("Product not found");

//                // Increase stock (IMPORTANT)
//                product.StockQuantity += item.Quantity;

//                var purchaseItem = new PurchaseItem
//                {
//                    ProductId = item.ProductId,
//                    Quantity = item.Quantity,
//                    PurchasePrice = item.PurchasePrice
//                };

//                total += item.Quantity * item.PurchasePrice;

//                purchase.Items.Add(purchaseItem);
//            }

//            purchase.TotalAmount = total;

//            _context.Purchases.Add(purchase);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<List<PurchaseDto>> GetAll()
//        {
//            return await _context.Purchases
//                .Include(x => x.Supplier)
//                .Select(x => new PurchaseDto
//                {
//                    Id = x.Id,
//                    SupplierName = x.Supplier.Name,
//                    PurchaseDate = x.PurchaseDate,
//                    TotalAmount = x.TotalAmount
//                })
//                .ToListAsync();
//        }

//        public async Task<PurchaseDetailsDto?> GetById(int id)
//        {
//            var purchase = await _context.Purchases
//                .Include(x => x.Supplier)
//                .Include(x => x.Items)
//                    .ThenInclude(i => i.Product)
//                .FirstOrDefaultAsync(x => x.Id == id);

//            if (purchase == null)
//                return null;

//            return new PurchaseDetailsDto
//            {
//                Id = purchase.Id,
//                SupplierName = purchase.Supplier.Name,
//                PurchaseDate = purchase.PurchaseDate,
//                TotalAmount = purchase.TotalAmount,
//                Items = purchase.Items.Select(i => new PurchaseItemDto
//                {
//                    ProductName = i.Product.Name,
//                    Quantity = i.Quantity,
//                    PurchasePrice = i.PurchasePrice,
//                    SubTotal = i.Quantity * i.PurchasePrice
//                }).ToList()
//            };
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Purchase;
using POS.Application.Interfaces.Purchase;
using POS.Domain.Entities;
using POS.Infrastructure.Data;

namespace POS.Infrastructure.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly AppDbContext _context;

        public PurchaseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatePurchase(CreatePurchaseDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                throw new Exception("At least one item is required");

            using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var purchase = new Purchase
                {
                    SupplierId = dto.SupplierId,
                    PurchaseDate = DateTime.UtcNow,
                    PurchaseNo = GeneratePurchaseNo(),
                    Items = new List<PurchaseItem>()
                };

                decimal subTotal = 0;

                foreach (var item in dto.Items)
                {
                    if (item.Quantity <= 0)
                        throw new Exception(
                            "Quantity must be greater than zero");

                    if (item.PurchasePrice <= 0)
                        throw new Exception(
                            "Purchase price must be greater than zero");

                    var product =
                        await _context.Products.FindAsync(item.ProductId);

                    if (product == null)
                        throw new Exception(
                            $"Product {item.ProductId} not found");

                    // STOCK INCREASE
                    product.StockQuantity += item.Quantity;

                    purchase.Items.Add(new PurchaseItem
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        PurchasePrice = item.PurchasePrice
                    });

                    subTotal +=
                        item.Quantity * item.PurchasePrice;
                }

                decimal discount = dto.Discount;

                if (discount < 0)
                    throw new Exception(
                        "Discount cannot be negative");

                if (discount > subTotal)
                    throw new Exception(
                        "Discount cannot exceed subtotal");

                decimal afterDiscount =
                    subTotal - discount;

                decimal taxPercentage =
                    dto.TaxPercentage;

                if (taxPercentage < 0)
                    throw new Exception(
                        "Tax cannot be negative");

                decimal taxAmount =
                    (afterDiscount * taxPercentage) / 100;

                decimal grandTotal =
                    afterDiscount + taxAmount;

                purchase.SubTotal = subTotal;
                purchase.Discount = discount;
                purchase.Tax = taxPercentage;
                purchase.TotalAmount = grandTotal;

                _context.Purchases.Add(purchase);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<PurchaseDto>> GetAll()
        {
            return await _context.Purchases
                .Include(x => x.Supplier)
                .Select(x => new PurchaseDto
                {
                    Id = x.Id,
                    PurchaseNo = x.PurchaseNo,
                    SupplierName = x.Supplier.Name,

                    SubTotal = x.SubTotal,
                    Discount = x.Discount,
                    Tax = x.Tax,
                    TotalAmount = x.TotalAmount,

                    PurchaseDate = x.PurchaseDate
                })
                .ToListAsync();
        }

        public async Task<PurchaseDetailsDto?> GetById(int id)
        {
            var purchase = await _context.Purchases
                .Include(x => x.Supplier)
                .Include(x => x.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (purchase == null)
                return null;

            return new PurchaseDetailsDto
            {
                Id = purchase.Id,
                PurchaseNo = purchase.PurchaseNo,
                SupplierName = purchase.Supplier.Name,

                SubTotal = purchase.SubTotal,
                Discount = purchase.Discount,
                Tax = purchase.Tax,
                TotalAmount = purchase.TotalAmount,

                PurchaseDate = purchase.PurchaseDate,

                Items = purchase.Items
                    .Select(i => new PurchaseItemDto
                    {
                        ProductName = i.Product.Name,
                        Quantity = i.Quantity,
                        PurchasePrice = i.PurchasePrice,
                        SubTotal =
                            i.Quantity * i.PurchasePrice
                    })
                    .ToList()
            };
        }

        private string GeneratePurchaseNo()
        {
            var nextId =
                (_context.Purchases.Count() + 1);

            return $"PUR-{nextId:D6}";
        }
    }
}