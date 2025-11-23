using NotificationService.Models;

namespace NotificationService.Services
{
    public interface INotificationService
    {
        void CreateNotification(int userId, string message);
        List<Notification> GetAll();
    }
}
