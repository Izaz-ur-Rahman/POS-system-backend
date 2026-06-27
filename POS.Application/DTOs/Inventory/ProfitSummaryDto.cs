using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Inventory
{
    public class ProfitSummaryDto
    {
        public decimal TotalSales { get; set; }
        public decimal CostOfGoodsSold { get; set; }
        public decimal GrossProfit { get; set; }
    }
}
