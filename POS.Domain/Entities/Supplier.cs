using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Domain.Entities
{
    public class Supplier
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? CompanyName { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
