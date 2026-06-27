using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Sale
{
    public class SaleDetailDto
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; } = "Walk-in Customer";

        public decimal SubTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }
        public decimal CashReceived { get; set; }
        public decimal ChangeReturn { get; set; }

        public DateTime SaleDate { get; set; }

        public List<SaleItemDto> Items { get; set; }
    }

    public class SaleItemDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal SalePrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
