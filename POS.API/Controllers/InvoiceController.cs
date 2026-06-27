using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Application.Interfaces.Invoice;
using POS.Infrastructure.Services;

namespace POS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 
    public class InvoiceController : ControllerBase
    {
        //private readonly IInvoiceService _service;

        //public InvoiceController(IInvoiceService service)
        //{
        //    _service = service;
        //}
        private readonly PdfService _pdfService;
        private readonly IInvoiceService _service;

        public InvoiceController(IInvoiceService service, PdfService pdfService)
        {
            _service = service;
            _pdfService = pdfService;
        }
        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetInvoice(int saleId)
        {
            var result = await _service.GetInvoiceBySaleId(saleId);

            if (result == null)
                return NotFound("Invoice not found");

            return Ok(result);
        }
        [HttpGet("{saleId}/pdf")]
        public async Task<IActionResult> GetInvoicePdf(int saleId)
        {
            try
            {
                var invoice = await _service.GetInvoiceBySaleId(saleId);

                if (invoice == null)
                    return NotFound();

                var pdfBytes = _pdfService.GenerateInvoicePdf(invoice);

                return File(pdfBytes, "application/pdf", $"{invoice.InvoiceNo}.pdf");
            }
            catch (Exception ex)
            {
                return StatusCode(500,
                    ex.ToString());
            }
        }
        //[HttpGet("{saleId}/pdf")]
        //public async Task<IActionResult> GetInvoicePdf(int saleId)
        //{
        //    var invoice = await _service.GetInvoiceBySaleId(saleId);

        //    if (invoice == null)
        //        return NotFound();

        //    var pdfService = new PdfService();
        //    var pdfBytes = pdfService.GenerateInvoicePdf(invoice);

        //    return File(pdfBytes, "application/pdf", $"{invoice.InvoiceNo}.pdf");
        //}

        //[HttpGet("{saleId}/print")]
        //public async Task<IActionResult> PrintInvoice(int saleId)
        //{
        //    var invoice = await _service.GetInvoiceBySaleId(saleId);

        //    if (invoice == null)
        //        return NotFound();

        //    return Ok(new
        //    {
        //        invoice.InvoiceNo,
        //        invoice.CustomerName,
        //        invoice.Date,
        //        invoice.Items,
        //        invoice.TotalAmount
        //    });
        //}
        [HttpGet("{saleId}/print")]
        public async Task<IActionResult> PrintInvoice(int saleId)
        {
            try { 
            var invoice = await _service.GetInvoiceBySaleId(saleId);

            if (invoice == null)
                return NotFound();

            return Ok(new
            {
                store = "MY POS STORE",
                invoice.InvoiceNo,
                invoice.CustomerName,
                invoice.Date,

                subTotal = invoice.SubTotal,   // 🔥 ADD THIS
                discount = invoice.Discount,
                tax = invoice.Tax,

                items = invoice.Items,
                totalAmount = invoice.TotalAmount
            });
        }
            catch (Exception ex)
            {
                return StatusCode(500,
                    ex.ToString());
            }
        }
    }
}