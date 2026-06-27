using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Reports;
using POS.Application.Interfaces.Reports;
using POS.Infrastructure.Data;

namespace POS.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DailySalesReportDto> GetDailySalesReport(DateTime date)
        {
            var sales = await _context.Sales
                .Where(x => x.SaleDate.Date == date.Date)
                .ToListAsync();

            return new DailySalesReportDto
            {
                Date = date,
                TotalTransactions = sales.Count,
                TotalSales = sales.Sum(x => x.TotalAmount),
                TotalCashReceived = sales.Sum(x => x.CashReceived),

                // simple profit estimation (you can improve later)
                TotalProfit = sales.Sum(x =>
                    x.TotalAmount - (x.SubTotal - x.Discount))
            };
        }

        public async Task<MonthlyRevenueDto> GetMonthlyRevenue(int year)
        {
            var sales = await _context.Sales
                .Where(x => x.SaleDate.Year == year)
                .ToListAsync();

            var monthlyData = sales
                .GroupBy(x => x.SaleDate.Month)
                .Select(g => new MonthlyRevenueItemDto
                {
                    Month = System.Globalization.CultureInfo
                        .CurrentCulture
                        .DateTimeFormat
                        .GetMonthName(g.Key),

                    TotalSales = g.Sum(x => x.TotalAmount)
                })
                .OrderBy(x => x.Month)
                .ToList();

            return new MonthlyRevenueDto
            {
                Year = year,
                MonthlyRevenue = monthlyData
            };
        }
    }
}