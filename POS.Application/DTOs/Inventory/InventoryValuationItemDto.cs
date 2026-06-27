using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Inventory
{
    public class InventoryValuationItemDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public int StockQuantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal Value { get; set; }
    }
}