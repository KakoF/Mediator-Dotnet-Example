using Viabilidade.Domain.Interfaces.Notifications;

namespace Viabilidade.Domain.Notifications
{
    public class NotificationHandler : INotificationHandler<Notification>
    {
        public List<Notification> _notifications { get; private set; }

        public NotificationHandler()
        {
            _notifications = new List<Notification>();
        }
        public bool HasNotification() => _notifications.Any();

        public string GetMessage() => _notifications.LastOrDefault().Message;

        public int GetStatusCode() => _notifications.LastOrDefault().StatusCode;

        public IReadOnlyCollection<Notification> GetNotifications() => _notifications;

        public List<object> GetNotificationsErrors() => _notifications.Select(x => x.Errors).ToList();

        public void AddNotification(string message, string error)
        {
            _notifications.Add(new Notification(message, error));
        }

        public void AddNotification(int statusCode, string message, string error)
        {
            _notifications.Add(new Notification(message, error, statusCode));
        }

        public void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotification(int statusCode, string message)
        {
            _notifications.Add(new Notification(message, null, statusCode));
        }
    }
}