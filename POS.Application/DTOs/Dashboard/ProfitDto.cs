using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Dashboard
{
    public class ProfitDto
    {
        public decimal TotalSales { get; set; }

        public decimal TotalPurchaseCost { get; set; }

        public decimal GrossProfit { get; set; }
    }
}