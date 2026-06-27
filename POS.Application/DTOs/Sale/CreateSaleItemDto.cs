using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Sale
{
    public class CreateSaleItemDto
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
