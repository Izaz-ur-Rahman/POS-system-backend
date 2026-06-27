using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Inventory
{
    public class DailyProfitDto
    {
        public DateTime Date { get; set; }
        public decimal Sales { get; set; }
        public decimal Cogs { get; set; }
        public decimal Profit { get; set; }
    }
}
