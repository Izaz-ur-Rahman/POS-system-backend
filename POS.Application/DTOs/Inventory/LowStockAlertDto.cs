using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Inventory
{
    public class LowStockAlertDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockQuantity { get; set; }
        public int MinStockLevel { get; set; }
        public string Status { get; set; }
    }
}
