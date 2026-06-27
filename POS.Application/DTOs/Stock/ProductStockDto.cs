using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Stock
{
    public class ProductStockDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        public int MinStockLevel { get; set; }

        public bool IsLowStock { get; set; }
    }
}
