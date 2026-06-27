using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Category
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}