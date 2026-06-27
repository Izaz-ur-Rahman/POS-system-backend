using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Inventory
{
    public class StockMovementDto
    {
        public string Type { get; set; } = string.Empty;

        public string ReferenceNo { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public int BalanceEffect { get; set; }

        public DateTime Date { get; set; }
    }
}