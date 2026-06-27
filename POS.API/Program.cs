//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using POS.Application.Interfaces;
//using POS.Application.Interfaces.Auth;
//using POS.Application.Interfaces.Dashboard;
//using POS.Application.Interfaces.Inventory;
//using POS.Application.Interfaces.Invoice;
//using POS.Application.Interfaces.Purchase;
//using POS.Application.Interfaces.Reports;
//using POS.Application.Interfaces.Sale;
//using POS.Infrastructure.Data;
//using POS.Infrastructure.Services;
//using QuestPDF.Infrastructure;
//using System.Text;

//QuestPDF.Settings.License = LicenseType.Community;
//var builder = WebApplication.CreateBuilder(args);


//// Ensure database folder exists
//var dbFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database");

//if (!Directory.Exists(dbFolder))
//{
//    Directory.CreateDirectory(dbFolder);
//}

//var dbPath = Path.Combine(dbFolder, "pos.db");

//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    options.UseSqlite($"Data Source={dbPath}");
//});

//// here is all services registered
//builder.Services.AddScoped<IJwtService, JwtService>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<ICustomerService, CustomerService>();
//builder.Services.AddScoped<ISupplierService, SupplierService>();
//builder.Services.AddScoped<IPurchaseService, PurchaseService>();
//builder.Services.AddScoped<ISaleService, SaleService>();
//builder.Services.AddScoped<IDashboardService, DashboardService>();
//builder.Services.AddScoped<IInventoryService,
//    InventoryService>();
//builder.Services.AddScoped<IReportService, ReportService>();
//builder.Services.AddScoped<IInvoiceService, InvoiceService>();
//builder.Services.AddScoped<PdfService>();
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = false,
//        ValidateAudience = false,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        IssuerSigningKey = new SymmetricSecurityKey(
//            Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345")
//        )
//    };
//});
//var app = builder.Build();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAll",
//        b => b.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader());
//});

//app.UseCors("AllowAll");
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseStaticFiles();
//app.UseHttpsRedirection();
//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using POS.Infrastructure.Hubs;
using POS.Application.Interfaces;
using POS.Application.Interfaces.Auth;
using POS.Application.Interfaces.Dashboard;
using POS.Application.Interfaces.Inventory;
using POS.Application.Interfaces.Invoice;
using POS.Application.Interfaces.Purchase;
using POS.Application.Interfaces.Reports;
using POS.Application.Interfaces.Sale;
using POS.Infrastructure.Data;
using POS.Infrastructure.Services;
using QuestPDF.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
if (!builder.Environment.IsDevelopment())
{
    builder.WebHost.UseUrls("http://0.0.0.0:5000");
}
builder.Services.AddSignalR();


QuestPDF.Settings.License = LicenseType.Community;

// DbContext
//builder.Services.AddDbContext<AppDbContext>(options =>
//{
//    options.UseSqlite($"Data Source={dbPath}");
//});
var dbFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

if (!Directory.Exists(dbFolder))
{
    Directory.CreateDirectory(dbFolder);
}

var dbPath = Path.Combine(dbFolder, "pos.db");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Data Source={dbPath}");
});
// Services
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<PdfService>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("THIS_IS_MY_SUPER_SECRET_KEY_12345")
        )
    };
});

// ✅ CORS MUST ALSO BE HERE
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        b => b.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

//var app = builder.Build();
////if (app.Environment.IsDevelopment())
////{
////    app.UseSwagger();
////    app.UseSwaggerUI();
////}
//app.UseSwagger();
//app.UseSwaggerUI();
//app.MapHub<NotificationHub>("/notificationHub"); 
//app.UseStaticFiles();
var app = builder.Build();


// ================= DEBUG START =================

Console.WriteLine("========================================");
Console.WriteLine("CURRENT DIRECTORY");
Console.WriteLine(Directory.GetCurrentDirectory());

Console.WriteLine("----------------------------------------");

Console.WriteLine("CONTENT ROOT");
Console.WriteLine(app.Environment.ContentRootPath);

Console.WriteLine("----------------------------------------");

Console.WriteLine("WEB ROOT");
Console.WriteLine(app.Environment.WebRootPath);

Console.WriteLine("----------------------------------------");

var imagePath = Path.Combine(
    app.Environment.WebRootPath,
    "uploads",
    "products",
    "003f9b68-e841-4e23-9dbd-804c3b01cdbd.png");

Console.WriteLine("IMAGE PATH");
Console.WriteLine(imagePath);

Console.WriteLine("----------------------------------------");

Console.WriteLine("IMAGE EXISTS");
Console.WriteLine(File.Exists(imagePath));

Console.WriteLine("========================================");

// ================= DEBUG END =================


app.UseSwagger();
app.UseSwaggerUI();

app.MapHub<NotificationHub>("/notificationHub");

app.UseStaticFiles();
//app.UseHttpsRedirection();

app.UseCors("AllowAll");   // 👈 IMPORTANT POSITION

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();