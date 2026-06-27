using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Dashboard;
using POS.Application.Interfaces.Dashboard;
using POS.Infrastructure.Data;

namespace POS.Infrastructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDbContext _context;

        public DashboardService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<DashboardSummaryDto> GetSummary()
        {
            var today = DateTime.UtcNow.Date;

            var todaySales =
                await _context.Sales
                    .Where(x => x.SaleDate.Date == today)
                    .SumAsync(x => (decimal?)x.TotalAmount)
                    ?? 0;

            var todayPurchases =
                await _context.Purchases
                    .Where(x => x.PurchaseDate.Date == today)
                    .SumAsync(x => (decimal?)x.TotalAmount)
                    ?? 0;

            var totalProducts =
                await _context.Products.CountAsync();

            var totalCustomers =
                await _context.Customers.CountAsync();

            var lowStockProducts =
                await _context.Products
                    .CountAsync(x =>
                        x.StockQuantity <= x.MinStockLevel);
            var monthStart =
    new DateTime(
        DateTime.UtcNow.Year,
        DateTime.UtcNow.Month,
        1);

            var monthSales =
                await _context.Sales
                    .Where(x => x.SaleDate >= monthStart)
                    .SumAsync(x => (decimal?)x.TotalAmount)
                    ?? 0;

            var monthPurchases =
                await _context.Purchases
                    .Where(x => x.PurchaseDate >= monthStart)
                    .SumAsync(x => (decimal?)x.TotalAmount)
                    ?? 0;

            return new DashboardSummaryDto
            {
                TodaySales = todaySales,
                TodayPurchases = todayPurchases,
                TotalProducts = totalProducts,
                TotalCustomers = totalCustomers,
                LowStockProducts = lowStockProducts,
                MonthPurchases = monthPurchases,
                MonthSales = monthSales
            };
        }
        public async Task<InventorySummaryDto> GetInventorySummary()
        {
            var products =
                await _context.Products.ToListAsync();

            return new InventorySummaryDto
            {
                TotalProducts =
                    products.Count,

                OutOfStockProducts =
                    products.Count(x => x.StockQuantity == 0),

                LowStockProducts =
                    products.Count(x =>
                        x.StockQuantity > 0 &&
                        x.StockQuantity <= x.MinStockLevel),

                HealthyProducts =
                    products.Count(x =>
                        x.StockQuantity > x.MinStockLevel),

                InventoryValue =
                    products.Sum(x =>
                        x.StockQuantity * x.PurchasePrice)
            };
        }

        public async Task<List<TopSellingProductDto>>
    GetTopSellingProducts(int top = 10)
        {
            return await _context.SaleItems
                .Include(x => x.Product)
                .GroupBy(x => new
                {
                    x.ProductId,
                    x.Product.Name
                })
                .Select(g => new TopSellingProductDto
                {
                    ProductId = g.Key.ProductId,

                    ProductName = g.Key.Name,

                    TotalQuantitySold =
                        g.Sum(x => x.Quantity),

                    TotalRevenue =
                        g.Sum(x =>
                            x.Quantity * x.SalePrice)
                })
                .OrderByDescending(x =>
                    x.TotalQuantitySold)
                .Take(top)
                .ToListAsync();
        }

        public async Task<List<SalesTrendDto>>
      GetSalesTrend(int days = 30)
        {
            var startDate =
                DateTime.UtcNow.Date.AddDays(-days);

            var sales = await _context.Sales
                .Where(x => x.SaleDate >= startDate)
                .ToListAsync();

            return sales
                .GroupBy(x => x.SaleDate.Date)
                .Select(g => new SalesTrendDto
                {
                    Date =
                        g.Key.ToString("yyyy-MM-dd"),

                    Sales =
                        g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Date)
                .ToList();
        }
        public async Task<ProfitDto> GetProfit()
        {
            var sales =
                await _context.SaleItems
                    .Include(x => x.Product)
                    .ToListAsync();

            decimal totalSales = sales
                .Sum(x => x.Quantity * x.SalePrice);

            decimal cogs = sales
                .Sum(x => x.Quantity * x.Product.PurchasePrice);

            decimal profit = totalSales - cogs;

            return new ProfitDto
            {
                TotalSales = totalSales,
                TotalPurchaseCost = cogs,
                GrossProfit = profit
            };
        }
    }
}