using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.DTOs.Invoice
{
    public class CompanyInfo
    {
        public string CompanyName { get; set; } = "MY POS STORE";

        public string Address { get; set; } =
            "Main Bazar, Swari ,District Buner,KPK, Pakistan";

        public string Phone { get; set; } =
            "+92-300-1234567";

        public string Footer { get; set; } =
            "Thank you for shopping!\nVisit Again";
    }
}
