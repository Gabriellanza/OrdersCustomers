using FluentValidation.Results;
using OrdersCustomers.Domain.Entities.Comum;
using OrdersCustomers.Domain.Interfaces.Comum;

namespace OrdersCustomers.Application.Services.Comum;

public sealed class NotificationService : INotificationService
{
    private List<Notification> _notifications;

    public NotificationService()
    {
        _notifications = new List<Notification>();
    }

    public void Handle(Notification notification, CancellationToken cancellationToken)
    {
        _notifications.Add(notification);
    }

    public void NewNotification(string key, string message, NotificationType type)
    {
        Handle(new Notification(key, message, type), default);
    }

    public void NotificationErrors<TEntity>(TEntity model) where TEntity : IModelValidator
    {
        var validationResult = (ValidationResult)model?.GetType()?.GetProperty("ValidationResult")?.GetValue(model, null);

        if (validationResult == null)
        {
            return;
        }

        foreach (var error in validationResult.Errors)
        {
            NewNotification(error.PropertyName, error.ErrorMessage, NotificationType.Error);
        }
    }

    public IEnumerable<Notification> GetNotifications()
    {
        return _notifications;
    }

    public bool HasNotifications()
    {
        return _notifications.Any(x => x.Type != NotificationType.Information);
    }

    public bool HasNotificationsErrors()
    {
        return _notifications.Any(x => x.Type == NotificationType.Error);
    }

    #region Dispose

    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed && _notifications.Count == 0)
        {
            return;
        }

        if (disposing)
        {
            _notifications = new List<Notification>();
        }

        _disposed = true;
    }

    #endregion
}