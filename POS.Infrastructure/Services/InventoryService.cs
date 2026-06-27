
using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Inventory;
using POS.Application.Interfaces.Inventory;
using POS.Infrastructure.Data;

namespace POS.Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockMovementDto>>
            GetStockMovement(int productId)
        {
            var purchaseMovements =
                await _context.PurchaseItems
                    .Include(x => x.Purchase)
                    .Where(x => x.ProductId == productId)
                    .Select(x => new StockMovementDto
                    {
                        Type = "Purchase",
                        ReferenceNo = x.Purchase.PurchaseNo ?? $"PUR-{x.PurchaseId}",
                        Quantity = x.Quantity,
                        BalanceEffect = x.Quantity,
                        Date = x.Purchase.PurchaseDate
                    })
                    .ToListAsync();

            var saleMovements =
                await _context.SaleItems
                    .Include(x => x.Sale)
                    .Where(x => x.ProductId == productId)
                    .Select(x => new StockMovementDto
                    {
                        Type = "Sale",
                        ReferenceNo = x.Sale.InvoiceNo ?? $"INV-{x.SaleId}",
                        Quantity = x.Quantity,
                        BalanceEffect = -x.Quantity,
                        Date = x.Sale.SaleDate
                    })
                    .ToListAsync();

            return purchaseMovements
                .Concat(saleMovements)
                .OrderByDescending(x => x.Date)
                .ToList();
        }
        public async Task<List<StockLedgerDto>> GetStockLedger(int productId)
        {
            var purchases = await _context.PurchaseItems
                .Include(x => x.Purchase)
                .Where(x => x.ProductId == productId)
                .Select(x => new StockLedgerDto
                {
                    Type = "IN",
                    Quantity = x.Quantity,
                    Reference = x.Purchase.PurchaseNo,
                    Date = x.Purchase.PurchaseDate
                })
                .ToListAsync();

            var sales = await _context.SaleItems
                .Include(x => x.Sale)
                .Where(x => x.ProductId == productId)
                .Select(x => new StockLedgerDto
                {
                    Type = "OUT",
                    Quantity = x.Quantity,
                    Reference = x.Sale.InvoiceNo,
                    Date = x.Sale.SaleDate
                })
                .ToListAsync();

            return purchases
                .Concat(sales)
                .OrderByDescending(x => x.Date)
                .ToList();
        }
       
        public async Task<InventoryValuationDto> GetInventoryValuation()
        {
            var products = await _context.Products.ToListAsync();

            var details = products.Select(p => new InventoryValuationItemDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                StockQuantity = p.StockQuantity,
                PurchasePrice = p.PurchasePrice,
                Value = p.StockQuantity * p.PurchasePrice
            }).ToList();

            return new InventoryValuationDto
            {
                TotalProducts = products.Count,
                TotalInventoryValue = details.Sum(x => x.Value),
                Details = details
            };
        }
        public async Task<List<LowStockDto>> GetLowStockProducts()
        {
            return await _context.Products
                .Where(p => p.StockQuantity <= p.MinStockLevel)
                .Select(p => new LowStockDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    CurrentStock = p.StockQuantity,
                    MinStockLevel = p.MinStockLevel
                })
                .ToListAsync();
        }
        public async Task<ProfitSummaryDto> GetProfitSummary()
        {
            var totalSales = await _context.Sales
                .SumAsync(x => x.TotalAmount);

            var cogs = await _context.SaleItems
                .Include(x => x.Product)
                .SumAsync(x => x.Quantity * x.Product.PurchasePrice);

            var grossProfit = totalSales - cogs;

            return new ProfitSummaryDto
            {
                TotalSales = totalSales,
                CostOfGoodsSold = cogs,
                GrossProfit = grossProfit
            };
        }

        public async Task<List<DailyProfitDto>> GetDailyProfit()
        {
            var sales = await _context.Sales
                .GroupBy(x => x.SaleDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Sales = g.Sum(x => x.TotalAmount)
                })
                .ToListAsync();

            var cogs = await _context.SaleItems
                .Include(x => x.Sale)
                .Include(x => x.Product)
                .GroupBy(x => x.Sale.SaleDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Cogs = g.Sum(x => x.Quantity * x.Product.PurchasePrice)
                })
                .ToListAsync();

            var result = sales.Select(s =>
            {
                var cost = cogs.FirstOrDefault(c => c.Date == s.Date)?.Cogs ?? 0;

                return new DailyProfitDto
                {
                    Date = s.Date,
                    Sales = s.Sales,
                    Cogs = cost,
                    Profit = s.Sales - cost
                };
            }).ToList();

            return result;
        }
        public async Task<object> GetProfitMargin()
        {
            var totalSales = await _context.Sales.SumAsync(x => x.TotalAmount);

            var cogs = await _context.SaleItems
                .Include(x => x.Product)
                .SumAsync(x => x.Quantity * x.Product.PurchasePrice);

            var profit = totalSales - cogs;

            var margin = totalSales == 0
                ? 0
                : (profit / totalSales) * 100;

            return new
            {
                TotalSales = totalSales,
                Cost = cogs,
                Profit = profit,
                MarginPercent = margin
            };
        }

        public async Task<List<ProductProfitDto>> GetProductProfitReport()
        {
            var products = await _context.Products.ToListAsync();

            var result = new List<ProductProfitDto>();

            foreach (var p in products)
            {
                var soldQty = await _context.SaleItems
                    .Where(x => x.ProductId == p.Id)
                    .SumAsync(x => x.Quantity);

                var salesRevenue = await _context.SaleItems
                    .Where(x => x.ProductId == p.Id)
                    .SumAsync(x => x.Quantity * x.SalePrice);

                var cogs = await _context.SaleItems
                    .Where(x => x.ProductId == p.Id)
                    .SumAsync(x => x.Quantity * p.PurchasePrice);

                result.Add(new ProductProfitDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    SoldQuantity = soldQty,
                    SalesRevenue = salesRevenue,
                    Cost = cogs,
                    Profit = salesRevenue - cogs
                });
            }

            return result;
        }
        public async Task<List<LowStockAlertDto>> GetReorderAlerts()
        {
            return await _context.Products
                .Where(p => p.StockQuantity <= p.MinStockLevel)
                .Select(p => new LowStockAlertDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    StockQuantity = p.StockQuantity,
                    MinStockLevel = p.MinStockLevel,
                    Status = "REORDER REQUIRED"
                })
                .ToListAsync();
        }
    }
}
