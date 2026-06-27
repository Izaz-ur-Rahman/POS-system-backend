using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class Purchase
    {
        public int Id { get; set; }

        public string PurchaseNo { get; set; } = string.Empty;

        public int SupplierId { get; set; }

        public Supplier Supplier { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }

        public List<PurchaseItem> Items { get; set; }
            = new();
    }
}
