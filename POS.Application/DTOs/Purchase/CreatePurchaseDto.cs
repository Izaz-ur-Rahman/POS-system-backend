using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Purchase
{
    public class CreatePurchaseDto
    {
        public int SupplierId { get; set; }

        public decimal Discount { get; set; }

        public decimal TaxPercentage { get; set; }

        public List<CreatePurchaseItemDto> Items { get; set; }
            = new();
    }
}
