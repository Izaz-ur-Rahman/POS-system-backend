using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Application.DTOs.Dashboard;

namespace POS.Application.Interfaces.Dashboard
{
    public interface IDashboardService
    {
        Task<InventorySummaryDto> GetInventorySummary();
        Task<DashboardSummaryDto> GetSummary();
        Task<List<TopSellingProductDto>> GetTopSellingProducts(int top = 3);
        Task<List<SalesTrendDto>> GetSalesTrend(int days = 30);
        Task<ProfitDto> GetProfit();

    }
}
