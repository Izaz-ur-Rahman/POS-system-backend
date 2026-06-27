using Microsoft.AspNetCore.SignalR;
using POS.Application.Interfaces;
using POS.Infrastructure.Hubs;

namespace POS.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hub;

        public NotificationService(IHubContext<NotificationHub> hub)
        {
            _hub = hub;
        }

        public async Task SendSaleNotification(string message)
        {
            await _hub.Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}