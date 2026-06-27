using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Barcode { get; set; } = string.Empty;

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public int StockQuantity { get; set; }

        public string? ImagePath { get; set; }

        public bool IsActive { get; set; }

        public string CategoryName { get; set; } = string.Empty;
    }

}