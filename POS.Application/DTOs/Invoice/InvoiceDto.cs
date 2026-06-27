using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Invoice
{
    public class InvoiceDto
    {
        public string InvoiceNo { get; set; }

        public int SaleId { get; set; }

        public string CustomerName { get; set; }

        public DateTime Date { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }

        public string QrCodeBase64 { get; set; }   // NEW

        public List<InvoiceItemDto> Items { get; set; }
        public decimal CashReceived { get; set; }
        public decimal ChangeReturn { get; set; }
        public decimal SubTotal { get; set; }
    }
}