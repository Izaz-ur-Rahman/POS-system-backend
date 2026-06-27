using System.Threading.Tasks;

namespace POS.Application.Interfaces
{
    public interface INotificationService
    {
        Task SendSaleNotification(string message);
    }
}