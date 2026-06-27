using MailKit.Net.Smtp;
using MimeKit;

namespace POS.Infrastructure.Services
{
    public class EmailService
    {
        public async Task SendInvoice(string email, byte[] pdf, string invoiceNo)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("POS System", "izazurrahman314@email.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = $"Invoice {invoiceNo}";

            var builder = new BodyBuilder
            {
                TextBody = "Please find your invoice attached"
            };

            builder.Attachments.Add($"{invoiceNo}.pdf", pdf);

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("your@email.com", "password");
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}