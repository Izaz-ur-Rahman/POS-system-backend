using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace POS.Application.DTOs.Supplier
{
    public class CreateSupplierDto
    {
        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? CompanyName { get; set; }
    }
}
