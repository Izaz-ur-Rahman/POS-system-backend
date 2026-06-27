using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Sale
{
    public class CreateSaleDto
    {
        public int? CustomerId { get; set; }

        public decimal Discount { get; set; }

        public decimal TaxPercentage { get; set; }
        //public decimal Tax { get; set; }        // percentage
        public decimal CashReceived { get; set; }

        public List<CreateSaleItemDto> Items { get; set; }
    }
}
//namespace POS.Application.DTOs.Sale
//{
//    public class CreateSaleDto
//    {
//        public int? CustomerId { get; set; }

//        public List<CreateSaleItemDto> Items { get; set; }
//    }
//}
