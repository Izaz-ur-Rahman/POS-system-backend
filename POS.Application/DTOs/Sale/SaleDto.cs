using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Sale
{
    public class SaleDto
    {
        public int Id { get; set; }

        public string InvoiceNo { get; set; }

        public string CustomerName { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Discount { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal CashReceived { get; set; }

        public decimal ChangeReturn { get; set; }

        public DateTime SaleDate { get; set; }
    }
}
//namespace POS.Application.DTOs.Sale
//{
//    public class SaleDto
//    {
//        public int Id { get; set; }

//        public string CustomerName { get; set; }

//        public DateTime SaleDate { get; set; }

//        public decimal TotalAmount { get; set; }
//    }
//}