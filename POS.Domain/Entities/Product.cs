using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Barcode { get; set; } = string.Empty;

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public int StockQuantity { get; set; }

        public string? ImagePath { get; set; }
      
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public int MinStockLevel { get; set; } = 5;
    }
}
