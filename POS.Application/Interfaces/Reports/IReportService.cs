using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Application.DTOs.Reports;

namespace POS.Application.Interfaces.Reports
{
    public interface IReportService
    {
        Task<DailySalesReportDto> GetDailySalesReport(DateTime date);
        Task<MonthlyRevenueDto> GetMonthlyRevenue(int year);
    }
}