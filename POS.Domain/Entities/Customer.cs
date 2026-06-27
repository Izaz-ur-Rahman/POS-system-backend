using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Address { get; set; }
        public bool IsDeleted { get; set; }

        public decimal CreditBalance { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
