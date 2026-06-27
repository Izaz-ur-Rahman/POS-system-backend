using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Application.DTOs.Invoice;

namespace POS.Application.Interfaces.Invoice
{
    public interface IInvoiceService
    {
        Task<InvoiceDto?> GetInvoiceBySaleId(int saleId);
    }
}