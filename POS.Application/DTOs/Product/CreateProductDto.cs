using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace POS.Application.DTOs.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;

        public string Barcode { get; set; } = string.Empty;

        public decimal PurchasePrice { get; set; }

        public decimal SalePrice { get; set; }

        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }

        public IFormFile? Image { get; set; }   // ✅ NEW
    }
}