using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Purchase
{
    public class PurchaseDto
    {
        public int Id { get; set; }

        public string PurchaseNo { get; set; }

        public string SupplierName { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime PurchaseDate { get; set; }
    }
}