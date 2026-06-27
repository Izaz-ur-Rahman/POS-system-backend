using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Inventory
{
    public class InventoryValuationDto
    {
        public int TotalProducts { get; set; }

        public decimal TotalInventoryValue { get; set; }

        public List<InventoryValuationItemDto> Details { get; set; }
            = new();
    }
}
