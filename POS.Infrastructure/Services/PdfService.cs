//using POS.Application.DTOs.Invoice;
//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

//namespace POS.Infrastructure.Services
//{
//    public class PdfService
//    {
//        private byte[] GetQrImage(string base64)
//        {
//            return Convert.FromBase64String(base64);
//        }
//        public byte[] GenerateInvoicePdf(InvoiceDto invoice)
//        {
//            return Document.Create(container =>
//            {
//                container.Page(page =>
//                {
//                    page.Margin(20);
//                    var logoPath = Path.Combine(
//    AppDomain.CurrentDomain.BaseDirectory,
//    "uploads",
//    "logo/logo.jpeg");

//                    var logoBytes = File.ReadAllBytes(logoPath);
//                    //page.Header().Text($"INVOICE: {invoice.InvoiceNo}")
//                    //    .FontSize(20).Bold();
//                    var company = new CompanyInfo();
//                    page.Header().Column(column =>
//                    {
//                        column.Item()
//    .AlignCenter()
//    .Image(logoBytes)
//    .FitHeight();
//                        column.Item()
//                            .Text(company.CompanyName)
//                            .FontSize(22)
//                            .Bold()
//                            .AlignCenter();

//                        column.Item()
//                            .Text(company.Address)
//                            .AlignCenter();

//                        column.Item()
//                            .Text(company.Phone)
//                            .AlignCenter();
//                    });
//                    page.Content().Column(col =>
//                    {
//                        //col.Item().Text($"Customer: {invoice.CustomerName}");
//                        //col.Item().Text($"Date: {invoice.Date}");
//                        //col.Item().Text($"Total: {invoice.TotalAmount}");
//                        col.Item().PaddingTop(15);

//                        col.Item().Text($"Invoice No : {invoice.InvoiceNo}");

//                        col.Item().Text($"Date : {invoice.Date}");

//                        col.Item().Text($"Customer : {invoice.CustomerName}");
//                        col.Item().PaddingTop(20);

//                        col.Item().Table(table =>
//                        {
//                            table.ColumnsDefinition(columns =>
//                            {
//                                columns.RelativeColumn(4);
//                                columns.RelativeColumn(1);
//                                columns.RelativeColumn(2);
//                                columns.RelativeColumn(2);
//                            });

//                            table.Header(header =>
//                            {
//                                header.Cell().Text("Product").Bold();
//                                header.Cell().Text("Qty").Bold();
//                                header.Cell().Text("Price").Bold();
//                                header.Cell().Text("Total").Bold();
//                            });

//                            foreach (var item in invoice.Items)
//                            {
//                                table.Cell().Text(item.ProductName);

//                                table.Cell().Text(item.Quantity.ToString());

//                                table.Cell().Text(item.Price.ToString("0.00"));

//                                table.Cell().Text(item.SubTotal.ToString("0.00"));
//                            }
//                        });
//                        col.Item().PaddingTop(15);

//                        col.Item()
//                            .AlignRight()
//                            .Text($"Grand Total : PKR {invoice.TotalAmount}")
//                            .FontSize(16)
//                            .Bold();
//                        col.Item()
//    .PaddingTop(20)
//    .AlignCenter()
//    .Image(GetQrImage(invoice.QrCodeBase64))
//    .FitWidth();
//                        page.Footer()
//    .AlignCenter()
//    .Text(company.Footer)
//    .FontSize(12);
//                        //col.Item().Table(table =>
//                        //{
//                        //    table.ColumnsDefinition(columns =>
//                        //    {
//                        //        columns.RelativeColumn();
//                        //        columns.ConstantColumn(50);
//                        //        columns.ConstantColumn(80);
//                        //    });

//                        //    table.Header(header =>
//                        //    {
//                        //        header.Cell().Text("Product");
//                        //        header.Cell().Text("Qty");
//                        //        header.Cell().Text("Price");
//                        //    });

//                        //    foreach (var item in invoice.Items)
//                        //    {
//                        //        table.Cell().Text(item.ProductName);
//                        //        table.Cell().Text(item.Quantity.ToString());
//                        //        table.Cell().Text(item.Price.ToString());
//                        //    }
//                        //});
//                    });
//                });
//            }).GeneratePdf();
//        }
//    }
//}

//using Microsoft.AspNetCore.Hosting;
//using POS.Application.DTOs.Invoice;
//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;

//namespace POS.Infrastructure.Services
//{
//    public class PdfService
//    {
//        private readonly IWebHostEnvironment _environment;

//        public PdfService(IWebHostEnvironment environment)
//        {
//            _environment = environment;
//        }
//        private byte[] GetQrImage(string base64)
//        {
//            return Convert.FromBase64String(base64);
//        }

//        public byte[] GenerateInvoicePdf(InvoiceDto invoice)
//        {
//            return Document.Create(container =>
//            {
//                container.Page(page =>
//                {
//                    // ✅ PAGE SETUP (A4)
//                    page.Size(PageSizes.A4);
//                    page.Margin(20);

//                    // ================= HEADER =================
//                    page.Header().Column(column =>
//                    {
//                        //var logoPath = Path.Combine(
//                        //    AppDomain.CurrentDomain.BaseDirectory,
//                        //    "uploads",
//                        //    "logo",
//                        //    "logo.jpeg");
//                        var logoPath = Path.Combine(
//    _environment.WebRootPath,
//    "uploads",
//    "logo",
//    "logo.jpeg");
//                        var logoBytes = File.ReadAllBytes(logoPath);

//                        column.Item()
//                            .AlignCenter()
//                            .Image(logoBytes)
//                            .FitHeight();

//                        column.Item()
//                            .Text("MY POS STORE")
//                            .FontSize(22)
//                            .Bold()
//                            .AlignCenter();

//                        column.Item()
//                            .Text("Main Bazar, Lahore, Pakistan")
//                            .AlignCenter();

//                        column.Item()
//                            .Text("+92-300-1234567")
//                            .AlignCenter();
//                    });

//                    // ================= CONTENT =================
//                    page.Content().Column(col =>
//                    {
//                        col.Item().PaddingTop(15);

//                        col.Item().Text($"Invoice No : {invoice.InvoiceNo}");
//                        col.Item().Text($"Date : {invoice.Date:dd-MM-yyyy hh:mm tt}");
//                        col.Item().Text($"Customer : {invoice.CustomerName}");

//                        col.Item().PaddingTop(20);

//                        // ================= TABLE =================
//                        col.Item().Table(table =>
//                        {
//                            table.ColumnsDefinition(columns =>
//                            {
//                                columns.RelativeColumn(4);
//                                columns.RelativeColumn(1);
//                                columns.RelativeColumn(2);
//                                columns.RelativeColumn(2);
//                            });

//                            // HEADER
//                            table.Header(header =>
//                            {
//                                header.Cell().Border(1).Padding(5).Text("Product").Bold();
//                                header.Cell().Border(1).Padding(5).Text("Qty").Bold();
//                                header.Cell().Border(1).Padding(5).Text("Price").Bold();
//                                header.Cell().Border(1).Padding(5).Text("Total").Bold();
//                            });

//                            // ITEMS
//                            foreach (var item in invoice.Items)
//                            {
//                                table.Cell().Border(1).Padding(5)
//                                    .Text(item.ProductName);

//                                table.Cell().Border(1).Padding(5)
//                                    .Text(item.Quantity.ToString());

//                                table.Cell().Border(1).Padding(5)
//                                    .Text(item.Price.ToString("0.00"));

//                                table.Cell().Border(1).Padding(5)
//                                    .Text(item.SubTotal.ToString("0.00"));
//                            }
//                        });

//                        // ================= TOTAL =================
//                        col.Item().PaddingTop(15);

//                        col.Item()
//                            .AlignRight()
//                            .Text($"Grand Total : PKR {invoice.TotalAmount:N2}")
//                            .FontSize(16)
//                            .Bold();

//                        // ================= QR CODE =================
//                        col.Item()
//                            .PaddingTop(20)
//                            .AlignCenter()
//                            .Width(120)
//                            .Height(120)
//                            .Image(GetQrImage(invoice.QrCodeBase64));
//                    });

//                    // ================= FOOTER =================
//                    page.Footer().Column(column =>
//                    {
//                        column.Item().LineHorizontal(1);

//                        column.Item()
//                            .PaddingTop(10)
//                            .AlignCenter()
//                            .Text("Thank you for shopping!")
//                            .FontSize(12);

//                        column.Item()
//                            .AlignCenter()
//                            .Text("Visit Again")
//                            .FontSize(12);
//                    });
//                });
//            }).GeneratePdf();
//        }
//    }
//}
using Microsoft.AspNetCore.Hosting;
using POS.Application.DTOs.Invoice;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace POS.Infrastructure.Services
{
    public class PdfService
    {
        private readonly IWebHostEnvironment _environment;

        public PdfService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        private byte[] GetQrImage(string base64)
        {
            return Convert.FromBase64String(base64);
        }

        public byte[] GenerateInvoicePdf(InvoiceDto invoice)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);

                    // ================= HEADER =================
                    page.Header().Row(row =>
                    {
                        // 🔥 SAFE LOGO PATH FIX
                        var logoPath = Path.Combine(
                            _environment.WebRootPath,
                            "uploads",
                            "logo",
                            "logo.jpeg"
                        );

                        byte[] logoBytes = File.Exists(logoPath)
                            ? File.ReadAllBytes(logoPath)
                            : Array.Empty<byte>();

                        row.RelativeItem().Column(col =>
                        {
                            if (logoBytes.Length > 0)
                            {
                                col.Item()
                                    .Height(60)
                                    .Image(logoBytes)
                                    .FitArea();
                            }

                            col.Item()
                                .Text("MY POS STORE")
                                .FontSize(18)
                                .Bold();

                            col.Item().Text("Main Bazar, Swari Buner,KPK,Pakistan");
                            col.Item().Text("+92-300-1234567");
                        });

                        row.ConstantItem(120).AlignRight().Column(col =>
                        {
                            col.Item().Text($"Invoice").Bold();
                            col.Item().Text(invoice.InvoiceNo);
                            col.Item().Text(invoice.Date.ToString("dd-MM-yyyy"));
                        });
                    });

                    // ================= ITEMS TABLE =================
                    page.Content().PaddingTop(10).Column(col =>
                    {
                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(4);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Product").Bold();
                                header.Cell().Text("Qty").Bold();
                                header.Cell().Text("Price").Bold();
                                header.Cell().Text("Total").Bold();
                            });

                            foreach (var item in invoice.Items)
                            {
                                table.Cell().Text(item.ProductName);
                                table.Cell().Text(item.Quantity.ToString());
                                table.Cell().Text(item.Price.ToString("0.00"));
                                table.Cell().Text(item.SubTotal.ToString("0.00"));
                            }
                        });

                        //col.Item().PaddingTop(15).AlignRight().Text($"Subtotal: {invoice.TotalAmount}");
                        col.Item().Text($"Subtotal: {invoice.SubTotal}");
                        col.Item().Text($"Discount: {invoice.Discount}");
                        col.Item().Text($"Tax: {invoice.Tax}%");
                      

                        col.Item()
                            .AlignRight()
                            .Text($"Grand Total: PKR {invoice.TotalAmount}")
                            .FontSize(14)
                            .Bold();

                        // ================= QR =================
                        if (!string.IsNullOrEmpty(invoice.QrCodeBase64))
                        {
                            col.Item()
                                .PaddingTop(20)
                                .AlignCenter()
                                .Height(100)
                                .Image(GetQrImage(invoice.QrCodeBase64));
                        }
                    });

                    // ================= FOOTER =================
                    page.Footer()
                        .AlignCenter()
                        .Text("Thank you for shopping with us!");
                });
            }).GeneratePdf();
        }
    }
}
// this one is working .... 
//using Microsoft.AspNetCore.Hosting;
//using POS.Application.DTOs.Invoice;
//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;

//namespace POS.Infrastructure.Services
//{
//    public class PdfService
//    {
//        private readonly IWebHostEnvironment _env;

//        public PdfService(IWebHostEnvironment env)
//        {
//            _env = env;
//        }

//        private byte[] GetQrImage(string base64)
//        {
//            return Convert.FromBase64String(base64);
//        }

//        public byte[] GenerateInvoicePdf(InvoiceDto invoice)
//        {
//            return Document.Create(container =>
//            {
//                container.Page(page =>
//                {
//                    page.Size(PageSizes.A4);
//                    page.Margin(20);

//                    // ================= HEADER =================
//                    page.Header().Column(column =>
//                    {
//                        // ✅ FIXED LOGO PATH
//                        var logoPath = Path.Combine(
//                            _env.WebRootPath,
//                            "uploads",
//                            "logo",
//                            "logo.jpeg"
//                        );

//                        byte[] logoBytes = File.Exists(logoPath)
//                            ? File.ReadAllBytes(logoPath)
//                            : Array.Empty<byte>();

//                        if (logoBytes.Length > 0)
//                        {
//                            column.Item()
//                                .AlignCenter()
//                                .Height(60)
//                                .Image(logoBytes)
//                                .FitHeight();
//                        }

//                        column.Item()
//                            .Text("MY POS STORE")
//                            .FontSize(18)
//                            .Bold()
//                            .AlignCenter();

//                        column.Item()
//                            .Text("Main Bazar, Lahore, Pakistan")
//                            .FontSize(10)
//                            .AlignCenter();

//                        column.Item()
//                            .Text("+92-300-1234567")
//                            .FontSize(10)
//                            .AlignCenter();
//                    });

//                    // ================= CONTENT =================
//                    page.Content().Column(col =>
//                    {
//                        col.Item().Text($"Invoice No: {invoice.InvoiceNo}").Bold();
//                        col.Item().Text($"Date: {invoice.Date:dd-MM-yyyy hh:mm tt}");
//                        col.Item().Text($"Customer: {invoice.CustomerName}");

//                        col.Item().PaddingTop(15);

//                        col.Item().Table(table =>
//                        {
//                            table.ColumnsDefinition(columns =>
//                            {
//                                columns.RelativeColumn(4);
//                                columns.RelativeColumn(1);
//                                columns.RelativeColumn(2);
//                                columns.RelativeColumn(2);
//                            });

//                            table.Header(header =>
//                            {
//                                header.Cell().Text("Product").Bold();
//                                header.Cell().Text("Qty").Bold();
//                                header.Cell().Text("Price").Bold();
//                                header.Cell().Text("Total").Bold();
//                            });

//                            foreach (var item in invoice.Items)
//                            {
//                                table.Cell().Text(item.ProductName);
//                                table.Cell().Text(item.Quantity.ToString());
//                                table.Cell().Text(item.Price.ToString("0.00"));
//                                table.Cell().Text(item.SubTotal.ToString("0.00"));
//                            }
//                        });

//                        col.Item().PaddingTop(15)
//                            .AlignRight()
//                            .Text($"Grand Total: PKR {invoice.TotalAmount:0.00}")
//                            .Bold();

//                        col.Item()
//                            .PaddingTop(20)
//                            .AlignCenter()
//                            .Width(120)
//                            .Height(120)
//                            .Image(GetQrImage(invoice.QrCodeBase64));
//                    });

//                    page.Footer().Column(column =>
//                    {
//                        column.Item().LineHorizontal(1);

//                        column.Item()
//                            .AlignCenter()
//                            .Text("Thank you for shopping!");

//                        column.Item()
//                            .AlignCenter()
//                            .Text("Visit Again");
//                    });
//                });
//            }).GeneratePdf();
//        }
//    }
//}
//using POS.Application.DTOs.Invoice;
//using QuestPDF.Fluent;
//using QuestPDF.Helpers;
//using QuestPDF.Infrastructure;

//namespace POS.Infrastructure.Services
//{
//    public class PdfService
//    {
//        private byte[] GetQrImage(string base64)
//        {
//            return Convert.FromBase64String(base64);
//        }

//        public byte[] GenerateInvoicePdf(InvoiceDto invoice)
//        {
//            return Document.Create(container =>
//            {
//                container.Page(page =>
//                {
//                    // ================= PAGE SETTINGS =================
//                    page.Size(PageSizes.A4);
//                    page.Margin(20);
//                    page.PageColor(Colors.White);

//                    // ================= HEADER =================
//                    page.Header().Column(column =>
//                    {
//                        // 🔥 SAFE LOGO PATH (IMPORTANT)
//                        var logoPath = Path.Combine(
//                            AppDomain.CurrentDomain.BaseDirectory,
//                            "wwwroot",
//                            "uploads",
//                            "logo",
//                            "logo.jpeg"
//                        );

//                        byte[] logoBytes = File.Exists(logoPath)
//                            ? File.ReadAllBytes(logoPath)
//                            : Array.Empty<byte>();

//                        // LOGO (SAFE SIZE FIX)
//                        if (logoBytes.Length > 0)
//                        {
//                            column.Item()
//                                .AlignCenter()
//                                .Height(60)
//                                .Image(logoBytes)
//                                .FitHeight();
//                        }

//                        column.Item()
//                            .Text("MY POS STORE")
//                            .FontSize(18)
//                            .Bold()
//                            .AlignCenter();

//                        column.Item()
//                            .Text("Main Bazar, Lahore, Pakistan")
//                            .FontSize(10)
//                            .AlignCenter();

//                        column.Item()
//                            .Text("+92-300-1234567")
//                            .FontSize(10)
//                            .AlignCenter();
//                    });

//                    // ================= CONTENT =================
//                    page.Content().Column(col =>
//                    {
//                        col.Item().PaddingTop(10);

//                        col.Item().Text($"Invoice No: {invoice.InvoiceNo}").Bold();
//                        col.Item().Text($"Date: {invoice.Date:dd-MM-yyyy hh:mm tt}");
//                        col.Item().Text($"Customer: {invoice.CustomerName}");

//                        col.Item().PaddingTop(15);

//                        // ================= TABLE =================
//                        col.Item().Table(table =>
//                        {
//                            table.ColumnsDefinition(columns =>
//                            {
//                                columns.RelativeColumn(4);
//                                columns.RelativeColumn(1);
//                                columns.RelativeColumn(2);
//                                columns.RelativeColumn(2);
//                            });

//                            // HEADER
//                            table.Header(header =>
//                            {
//                                header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Product").Bold();
//                                header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Qty").Bold();
//                                header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Price").Bold();
//                                header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text("Total").Bold();
//                            });

//                            // ITEMS
//                            foreach (var item in invoice.Items)
//                            {
//                                table.Cell().Padding(5).Text(item.ProductName);
//                                table.Cell().Padding(5).Text(item.Quantity.ToString());
//                                table.Cell().Padding(5).Text(item.Price.ToString("0.00"));
//                                table.Cell().Padding(5).Text(item.SubTotal.ToString("0.00"));
//                            }
//                        });

//                        // ================= TOTAL =================
//                        col.Item().PaddingTop(15);

//                        col.Item()
//                            .AlignRight()
//                            .Text($"Grand Total: PKR {invoice.TotalAmount:0.00}")
//                            .FontSize(14)
//                            .Bold();

//                        // ================= QR CODE =================
//                        col.Item()
//                            .PaddingTop(20)
//                            .AlignCenter()
//                            .Height(100)
//                            .Width(100)
//                            .Image(GetQrImage(invoice.QrCodeBase64));
//                    });

//                    // ================= FOOTER =================
//                    page.Footer().Column(column =>
//                    {
//                        column.Item().LineHorizontal(1);

//                        column.Item()
//                            .PaddingTop(8)
//                            .AlignCenter()
//                            .Text("Thank you for shopping!")
//                            .FontSize(10);

//                        column.Item()
//                            .AlignCenter()
//                            .Text("Visit Again")
//                            .FontSize(10);
//                    });
//                });
//            }).GeneratePdf();
//        }
//    }
//}