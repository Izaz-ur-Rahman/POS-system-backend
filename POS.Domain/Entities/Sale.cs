using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }

        public string InvoiceNo { get; set; }

        public int? CustomerId { get; set; }

        public Customer Customer { get; set; }

        public DateTime SaleDate { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal CashReceived { get; set; }

        public decimal ChangeReturn { get; set; }

        public List<SaleItem> Items { get; set; }
    }
}
//namespace POS.Domain.Entities
//{
//    public class Sale
//    {
//        public int Id { get; set; }

//        public int? CustomerId { get; set; }
//        public Customer Customer { get; set; }

//        public DateTime SaleDate { get; set; } = DateTime.UtcNow;

//        public decimal TotalAmount { get; set; }

//        public List<SaleItem> Items { get; set; }
//    }
//}