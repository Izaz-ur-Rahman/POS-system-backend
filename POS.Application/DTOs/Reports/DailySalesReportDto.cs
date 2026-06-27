using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Reports
{
    public class DailySalesReportDto
    {
        public DateTime Date { get; set; }

        public int TotalTransactions { get; set; }

        public decimal TotalSales { get; set; }

        public decimal TotalCashReceived { get; set; }

        public decimal TotalProfit { get; set; }
    }
}
