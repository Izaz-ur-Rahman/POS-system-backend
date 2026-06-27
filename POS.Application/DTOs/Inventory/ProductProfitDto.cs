using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Inventory
{
    public class ProductProfitDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int SoldQuantity { get; set; }
        public decimal SalesRevenue { get; set; }
        public decimal Cost { get; set; }
        public decimal Profit { get; set; }
    }
}
