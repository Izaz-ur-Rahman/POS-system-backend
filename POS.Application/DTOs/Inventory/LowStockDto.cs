using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Inventory
{
    public class LowStockDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CurrentStock { get; set; }
        public int MinStockLevel { get; set; }
    }
}