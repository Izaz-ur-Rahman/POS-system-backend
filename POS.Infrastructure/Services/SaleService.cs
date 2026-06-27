//using Microsoft.EntityFrameworkCore;
//using POS.Application.DTOs.Sale;
//using POS.Application.Interfaces.Sale;
//using POS.Domain.Entities;
//using POS.Infrastructure.Data;

//namespace POS.Infrastructure.Services
//{
//    public class SaleService : ISaleService
//    {
//        private readonly AppDbContext _context;

//        public SaleService(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task CreateSale(CreateSaleDto dto)
//        {
//            var sale = new Sale
//            {
//                CustomerId = dto.CustomerId,
//                SaleDate = DateTime.UtcNow,
//                Items = new List<SaleItem>()
//            };

//            //decimal total = 0;
//            decimal subTotal = 0;

//            foreach (var item in dto.Items)
//            {
//                var product = await _context.Products
//                    .FirstOrDefaultAsync(x => x.Id == item.ProductId);

//                if (product == null)
//                    throw new Exception("Product not found");
//                if (item.Quantity <= 0)
//                    throw new Exception("Quantity must be greater than zero");
//                // 1️⃣ STOCK CHECK (REAL POS RULE)
//                ValidateStock(product, item.Quantity);

//                // 2️⃣ STOCK REDUCTION (ONLY AFTER VALIDATION)
//                product.StockQuantity -= item.Quantity;

//                // 3️⃣ PRICE CALCULATION (LOCK PRICE AT SALE TIME)
//                var salePrice = product.SalePrice;

//                var saleItem = new SaleItem
//                {
//                    ProductId = product.Id,
//                    Quantity = item.Quantity,
//                    SalePrice = salePrice
//                };

//                sale.Items.Add(saleItem);

//                // total += item.Quantity * salePrice;
//                subTotal += item.Quantity * product.SalePrice;
//            }

//            // 4️⃣ FINAL TOTAL
//            sale.TotalAmount = subTotal;

//            // 5️⃣ SAVE TRANSACTION
//            _context.Sales.Add(sale);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<List<SaleDto>> GetAll()
//        {
//            return await _context.Sales
//                .Include(x => x.Customer)
//                .Select(x => new SaleDto
//                {
//                    Id = x.Id,
//                    CustomerName = x.Customer != null ? x.Customer.Name : "Walk-in",
//                    SaleDate = x.SaleDate,
//                    TotalAmount = x.TotalAmount
//                })
//                .ToListAsync();
//        }
//        private void ValidateStock(Product product, int requestedQty)
//        {
//            if (product.StockQuantity < requestedQty)
//            {
//                throw new Exception(
//                    $"Insufficient stock for {product.Name}. Available: {product.StockQuantity}"
//                );
//            }
//        }
//    }
//}

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using POS.Application;
using POS.Application.DTOs.Sale;
using POS.Application.Interfaces;
using POS.Application.Interfaces.Sale;
using POS.Domain.Entities;
using POS.Infrastructure.Data;

namespace POS.Infrastructure.Services
{
    public class SaleService : ISaleService
    {
        private readonly AppDbContext _context;
        private readonly INotificationService _notification;
        public SaleService(AppDbContext context, INotificationService notification)
        {
            _context = context;
            _notification = notification;
        }
        public async Task CreateSale(CreateSaleDto dto)
        {
            if (dto.Items == null || dto.Items.Count == 0)
                throw new Exception("Sale must contain at least one item");
            if (dto.Items == null || !dto.Items.Any())
                throw new Exception("At least one item is required");
            //var sale = new Sale
            //{
            //    CustomerId = dto.CustomerId,
            //    SaleDate = DateTime.UtcNow,
            //    Items = new List<SaleItem>()
            //};
            var sale = new Sale
            {
                CustomerId = dto.CustomerId,
                SaleDate = DateTime.UtcNow,
                Items = new List<SaleItem>(),

                // ✅ FIX HERE
                InvoiceNo = GenerateInvoiceNo()
            };
            decimal subTotal = 0;

            foreach (var item in dto.Items)
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == item.ProductId);

                if (product == null)
                    throw new Exception("Product not found");

                if (item.Quantity <= 0)
                    throw new Exception("Quantity must be greater than zero");

                ValidateStock(product, item.Quantity);

                product.StockQuantity -= item.Quantity;

                var salePrice = product.SalePrice;

                sale.Items.Add(new SaleItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    SalePrice = salePrice
                });

                subTotal += item.Quantity * salePrice;
            }

            // ================= DISCOUNT =================
            decimal discountAmount = dto.Discount;

            if (discountAmount < 0)
                throw new Exception("Discount cannot be negative");

            if (discountAmount > subTotal)
                throw new Exception("Discount cannot exceed subtotal");

            decimal afterDiscount = subTotal - discountAmount;

            // ================= TAX =================
            decimal taxPercent = dto.TaxPercentage;

            if (taxPercent < 0)
                throw new Exception("Tax cannot be negative");

            decimal taxAmount = (afterDiscount * taxPercent) / 100;

            // ================= GRAND TOTAL =================
            decimal grandTotal = afterDiscount + taxAmount;

            // ================= CASH CALCULATION (NEW IMPORTANT PART) =================
            if (dto.CashReceived < grandTotal)
                throw new Exception("Insufficient cash received");

            decimal changeReturn = dto.CashReceived - grandTotal;

            // ================= ASSIGN =================
            sale.SubTotal = subTotal;
            sale.Discount = discountAmount;
            sale.Tax = taxPercent;
            sale.TotalAmount = grandTotal;

            // OPTIONAL (if you added fields in DB)
            sale.CashReceived = dto.CashReceived;
            sale.ChangeReturn = changeReturn;

            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            await _notification.SendSaleNotification(
    $"New Sale Created: {sale.InvoiceNo}"
);
        }
        //public async Task CreateSale(CreateSaleDto dto)
        //{
        //    var sale = new Sale
        //    {
        //        CustomerId = dto.CustomerId,
        //        SaleDate = DateTime.UtcNow,
        //        Items = new List<SaleItem>()
        //    };

        //    decimal subTotal = 0;

        //    foreach (var item in dto.Items)
        //    {
        //        var product = await _context.Products
        //            .FirstOrDefaultAsync(x => x.Id == item.ProductId);

        //        if (product == null)
        //            throw new Exception("Product not found");

        //        if (item.Quantity <= 0)
        //            throw new Exception("Quantity must be greater than zero");

        //        ValidateStock(product, item.Quantity);

        //        // stock deduction
        //        product.StockQuantity -= item.Quantity;

        //        var salePrice = product.SalePrice;

        //        sale.Items.Add(new SaleItem
        //        {
        //            ProductId = product.Id,
        //            Quantity = item.Quantity,
        //            SalePrice = salePrice
        //        });

        //        subTotal += item.Quantity * salePrice;
        //    }

        //    // ===============================
        //    // 💰 DISCOUNT (FLAT AMOUNT)
        //    // ===============================
        //    decimal discountAmount = dto.Discount;

        //    if (discountAmount < 0)
        //        throw new Exception("Discount cannot be negative");

        //    if (discountAmount > subTotal)
        //        throw new Exception("Discount cannot exceed subtotal");

        //    decimal afterDiscount = subTotal - discountAmount;

        //    // ===============================
        //    // 🧾 TAX (GST %)
        //    // ===============================
        //    decimal taxPercent = dto.Tax;

        //    if (taxPercent < 0)
        //        throw new Exception("Tax cannot be negative");

        //    decimal taxAmount = (afterDiscount * taxPercent) / 100;

        //    // ===============================
        //    // 💳 FINAL TOTAL
        //    // ===============================
        //    decimal grandTotal = afterDiscount + taxAmount;

        //    sale.SubTotal = subTotal;
        //    sale.Discount = discountAmount;
        //    sale.Tax = taxPercent;
        //    sale.TotalAmount = grandTotal;

        //    // ===============================
        //    // 💾 SAVE TRANSACTION
        //    // ===============================
        //    _context.Sales.Add(sale);
        //    await _context.SaveChangesAsync();
        //}
        public async Task<List<SaleDto>> GetAll()
        {
            return await _context.Sales
                .Include(x => x.Customer)
                .Select(x => new SaleDto
                {
                    Id = x.Id,
                    InvoiceNo = x.InvoiceNo,

                    CustomerName = x.Customer != null ? x.Customer.Name : "Walk-in Customer",

                    SubTotal = x.SubTotal,
                    Discount = x.Discount,
                    Tax = x.Tax,

                    TotalAmount = x.TotalAmount,
                    CashReceived = x.CashReceived,
                    ChangeReturn = x.ChangeReturn,

                    SaleDate = x.SaleDate
                })
                .ToListAsync();
        }
        //public async Task<List<SaleDto>> GetAll()
        //{
        //    return await _context.Sales
        //        .Include(x => x.Customer)
        //        .Select(x => new SaleDto
        //        {
        //            Id = x.Id,
        //            CustomerName = x.Customer != null ? x.Customer.Name : "Walk-in",
        //            SaleDate = x.SaleDate,
        //            TotalAmount = x.TotalAmount
        //        })
        //        .ToListAsync();
        //}
        public async Task<SaleDetailDto> GetById(int id)
        {
            var sale = await _context.Sales
                .Include(x => x.Customer)
                .Include(x => x.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (sale == null)
                throw new Exception("Sale not found");

            return new SaleDetailDto
            {
                Id = sale.Id,
                InvoiceNo = sale.InvoiceNo,
                CustomerName = sale.Customer != null ? sale.Customer.Name : "Walk-in Customer",

                SubTotal = sale.SubTotal,
                Discount = sale.Discount,
                Tax = sale.Tax,
                TotalAmount = sale.TotalAmount,
                CashReceived = sale.CashReceived,
                ChangeReturn = sale.ChangeReturn,
                SaleDate = sale.SaleDate,

                Items = sale.Items.Select(i => new SaleItemDto
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product != null ? i.Product.Name : "",
                    Quantity = i.Quantity,
                    SalePrice = i.SalePrice,
                    SubTotal = i.Quantity * i.SalePrice
                }).ToList()
            };
        }
        // ===============================
        // 🔐 STOCK VALIDATION (ENTERPRISE RULE)
        // ===============================
        private void ValidateStock(Product product, int requestedQty)
        {
            if (product.StockQuantity < requestedQty)
            {
                throw new Exception(
                    $"Insufficient stock for {product.Name}. Available: {product.StockQuantity}"
                );
            }
        }

        private string GenerateInvoiceNo()
        {
            return $"INV-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }
}