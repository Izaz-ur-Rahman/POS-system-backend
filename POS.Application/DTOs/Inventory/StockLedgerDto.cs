using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Inventory
{
    public class StockLedgerDto
    {
        public string Type { get; set; }   // IN / OUT
        public int Quantity { get; set; }
        public string Reference { get; set; }
        public DateTime Date { get; set; }
    }
}