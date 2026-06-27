using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Product
{
    using Microsoft.AspNetCore.Http;

    public class UpdateProductDto
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal SalePrice { get; set; }
        public int StockQuantity { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }

        public IFormFile? Image { get; set; }
    }
}
