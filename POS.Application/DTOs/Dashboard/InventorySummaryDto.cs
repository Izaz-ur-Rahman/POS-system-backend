using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Dashboard
{
    public class InventorySummaryDto
    {
        public int TotalProducts { get; set; }

        public int OutOfStockProducts { get; set; }

        public int LowStockProducts { get; set; }

        public int HealthyProducts { get; set; }

        public decimal InventoryValue { get; set; }
    }
}
