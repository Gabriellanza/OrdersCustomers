using OrdersCustomers.Domain.Entities.Comum;

namespace OrdersCustomers.Domain.Interfaces.Comum;

public interface INotificationService : IDisposable
{
    void Handle(Notification notification, CancellationToken cancellationToken);

    void NewNotification(string key, string message, NotificationType type);

    void NotificationErrors<TEntity>(TEntity model) where TEntity : IModelValidator;

    IEnumerable<Notification> GetNotifications();

    bool HasNotifications();

    bool HasNotificationsErrors();
}