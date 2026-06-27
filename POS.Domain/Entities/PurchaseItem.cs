using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Domain.Entities
{
    public class PurchaseItem
    {
        public int Id { get; set; }

        public int PurchaseId { get; set; }
        public Purchase Purchase { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal SubTotal => Quantity * PurchasePrice;
    }
}
