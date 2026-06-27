namespace POS.Application.DTOs.Reports
{
    public class MonthlyRevenueDto
    {
        public int Year { get; set; }

        public List<MonthlyRevenueItemDto> MonthlyRevenue { get; set; }
            = new();
    }
}