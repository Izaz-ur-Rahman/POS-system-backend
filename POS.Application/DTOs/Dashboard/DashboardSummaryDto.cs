using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        public decimal TodaySales { get; set; }

        public decimal TodayPurchases { get; set; }

        public int TotalProducts { get; set; }
        public decimal MonthSales { get; set; }
        public decimal MonthPurchases { get; set; }
        public int TotalCustomers { get; set; }

        public int LowStockProducts { get; set; }
    }
}