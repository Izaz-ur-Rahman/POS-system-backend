using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Purchase
{
    public class PurchaseItemDto
    {
        public string ProductName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SubTotal { get; set; }
    }
}
