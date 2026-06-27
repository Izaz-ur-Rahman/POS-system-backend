using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Dashboard
{
    public class SalesTrendDto
    {
        public string Date { get; set; } = string.Empty;

        public decimal Sales { get; set; }
    }
}