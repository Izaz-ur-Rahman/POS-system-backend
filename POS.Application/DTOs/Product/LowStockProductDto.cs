using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Product
{
    public class LowStockProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        public int MinStockLevel { get; set; }

        public int Shortage =>
            MinStockLevel - StockQuantity;
    }
}
