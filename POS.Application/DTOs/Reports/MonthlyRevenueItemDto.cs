namespace POS.Application.DTOs.Reports
{
    public class MonthlyRevenueItemDto
    {
        public string Month { get; set; } = string.Empty;

        public decimal TotalSales { get; set; }
    }
}