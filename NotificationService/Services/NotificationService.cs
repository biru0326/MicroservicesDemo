using NotificationService.Data;
using NotificationService.Models;

namespace NotificationService.Services
{
    public class NotificationService : INotificationService
    {
        private readonly NotificationDbContext _context;

        public NotificationService(NotificationDbContext context) => _context = context;

        public void CreateNotification(int userId, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                Message = message
            };
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public List<Notification> GetAll() => _context.Notifications.ToList();
    }
}
