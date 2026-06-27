
//using Microsoft.EntityFrameworkCore;
//using POS.Application.DTOs.Invoice;
//using POS.Application.Interfaces.Invoice;
//using POS.Infrastructure.Data;
//using QRCoder;
//using System.Drawing;
//using System.IO;
//namespace POS.Infrastructure.Services
//{
//    public class InvoiceService : IInvoiceService
//    {
//        private readonly AppDbContext _context;

//        public InvoiceService(AppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<InvoiceDto?> GetInvoiceBySaleId(int saleId)
//        {
//            var sale = await _context.Sales
//                .Include(x => x.Customer)
//                .Include(x => x.Items)
//                    .ThenInclude(i => i.Product)
//                .FirstOrDefaultAsync(x => x.Id == saleId);

//            if (sale == null)
//                return null;

//            // 🧾 INVOICE NUMBER GENERATION
//            string invoiceNo = $"INV-{sale.Id.ToString("D6")}";
//            string qrText = $"Invoice:{invoiceNo}-Sale:{sale.Id}-Total:{sale.TotalAmount}";
//            string qrBase64 = GenerateQrCode(qrText);
//            var invoice = new InvoiceDto
//            {
//                InvoiceNo = invoiceNo,
//                SaleId = sale.Id,
//                CustomerName = sale.Customer != null ? sale.Customer.Name : "Walk-in Customer",
//                Date = sale.SaleDate,
//                TotalAmount = sale.TotalAmount,
//                QrCodeBase64 = qrBase64,
//                Items = sale.Items.Select(i => new InvoiceItemDto
//                {
//                    ProductName = i.Product.Name,
//                    Quantity = i.Quantity,
//                    Price = i.SalePrice,
//                    SubTotal = i.Quantity * i.SalePrice

//                }).ToList()
//            };

//            return invoice;
//        }

//        // QR generator method
//        private string GenerateQrCode(string text)
//        {
//            using var qrGenerator = new QRCodeGenerator();
//            var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
//            var qrCode = new PngByteQRCode(qrCodeData);

//            byte[] qrBytes = qrCode.GetGraphic(20);

//            return Convert.ToBase64String(qrBytes);
//        }
//    }
//}

using Microsoft.EntityFrameworkCore;
using POS.Application.DTOs.Invoice;
using POS.Application.Interfaces.Invoice;
using POS.Infrastructure.Data;
using QRCoder;

namespace POS.Infrastructure.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly AppDbContext _context;

        public InvoiceService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<InvoiceDto> GetInvoiceBySaleId(int saleId)
        {
            var sale = await _context.Sales
                .Include(x => x.Customer)
                .Include(x => x.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(x => x.Id == saleId);

            if (sale == null)
                return null;
            string qrText =
$@"Invoice : {sale.InvoiceNo}
Customer : {(sale.Customer != null ? sale.Customer.Name : "Walk-in Customer")}
Total : {sale.TotalAmount}
Date : {sale.SaleDate:dd-MM-yyyy}";

            string qr = GenerateQrCode(qrText);

            return new InvoiceDto
            {
                InvoiceNo = sale.InvoiceNo,

                CustomerName =
                    sale.Customer != null
                    ? sale.Customer.Name
                    : "Walk-in Customer",

                SubTotal = sale.SubTotal,
                Discount = sale.Discount,
                Tax = sale.Tax,

                TotalAmount = sale.TotalAmount,

                CashReceived = sale.CashReceived,
                ChangeReturn = sale.ChangeReturn,

                Date = sale.SaleDate,

                QrCodeBase64 = qr,

                Items = sale.Items.Select(i => new InvoiceItemDto
                {
                    ProductName = i.Product.Name,
                    Quantity = i.Quantity,
                    Price = i.SalePrice,
                    SubTotal = i.Quantity * i.SalePrice
                }).ToList()
            };
            //return new InvoiceDto
            //{
            //    InvoiceNo = sale.InvoiceNo,
            //    CustomerName = sale.Customer != null ? sale.Customer.Name : "Walk-in",

            //    SubTotal = sale.SubTotal,        // 🔥 MUST
            //    Discount = sale.Discount,        // 🔥 MUST
            //    Tax = sale.Tax,                  // 🔥 MUST

            //    TotalAmount = sale.TotalAmount,
            //    CashReceived = sale.CashReceived,
            //    ChangeReturn = sale.ChangeReturn,

            //    Date = sale.SaleDate,

            //    Items = sale.Items.Select(i => new InvoiceItemDto
            //    {
            //        ProductName = i.Product.Name,
            //        Quantity = i.Quantity,
            //        Price = i.SalePrice,
            //        SubTotal = i.Quantity * i.SalePrice
            //    }).ToList()
            //};
        }
        //        public async Task<InvoiceDto?> GetInvoiceBySaleId(int saleId)
        //        {
        //            var sale = await _context.Sales
        //                .Include(x => x.Customer)
        //                .Include(x => x.Items)
        //                    .ThenInclude(i => i.Product)
        //                .FirstOrDefaultAsync(x => x.Id == saleId);

        //            if (sale == null)
        //                return null;

        //            string invoiceNo = $"INV-{sale.Id.ToString("D6")}";

        //            // ✅ CLEAN QR TEXT (PROFESSIONAL)
        //            string qrText =
        //$@"Invoice : {invoiceNo}
        //Customer : {(sale.Customer != null ? sale.Customer.Name : "Walk-in")}
        //Total : {sale.TotalAmount:N2}
        //Date : {sale.SaleDate:dd-MM-yyyy}";

        //            string qrBase64 = GenerateQrCode(qrText);

        //            return new InvoiceDto
        //            {
        //                InvoiceNo = invoiceNo,
        //                SaleId = sale.Id,
        //                CustomerName = sale.Customer != null ? sale.Customer.Name : "Walk-in Customer",
        //                Date = sale.SaleDate,
        //                TotalAmount = sale.TotalAmount,
        //                QrCodeBase64 = qrBase64,

        //                Items = sale.Items.Select(i => new InvoiceItemDto
        //                {
        //                    ProductName = i.Product.Name,
        //                    Quantity = i.Quantity,
        //                    Price = i.SalePrice,
        //                    SubTotal = i.Quantity * i.SalePrice
        //                }).ToList()
        //            };
        //        }

        private string GenerateQrCode(string text)
        {
            using var qrGenerator = new QRCodeGenerator();

            var qrCodeData = qrGenerator.CreateQrCode(
                text,
                QRCodeGenerator.ECCLevel.Q);

            var qrCode = new PngByteQRCode(qrCodeData);

            byte[] qrBytes = qrCode.GetGraphic(20);

            return Convert.ToBase64String(qrBytes);
        }
    }
}