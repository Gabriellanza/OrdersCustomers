using Microsoft.AspNetCore.Mvc;
using OrdersCustomers.Application.Interfaces.Comum;
using OrdersCustomers.Application.ViewModels;
using OrdersCustomers.Domain.Entities.Comum;

namespace OrdersCustomers.Controllers;

public abstract class BaseController : ControllerBase
{
    protected readonly INotificationService Notifications;

    protected BaseController(IServiceProvider serviceProvider)
    {
        Notifications = serviceProvider.GetService<INotificationService>();
    }

    
    #region Notifications

    protected void NewNotification(string key, string message, NotificationType type = NotificationType.Error) => Notifications.NewNotification(key, message, type);

    protected void NotificationErrors<TEntity>(TEntity model) where TEntity : IModelValidator? => Notifications.NotificationErrors(model);

    #endregion

    protected bool OperacaoValida()
    {
        return !Notifications.HasNotificationsErrors();
    }
}

public abstract class ApiBaseController : BaseController
{
    protected ApiBaseController(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected IActionResult ResponseGeneric<T>(T result)
    {
        if (!Notifications.HasNotificationsErrors())
        {
            return Ok(new ViewModel<T>(result, Notifications.GetNotifications()));
        }

        return BadRequest(new ViewModel<T>
        (
            path: Request.Path.Value,
            remoteAddress: HttpContext?.Connection.RemoteIpAddress?.ToString(),
            notifications: Notifications.GetNotifications()
        ));
    }

    protected IActionResult CreateResponse<T>(T result) where T : IComparable
    {
        if (!Notifications.HasNotifications())
        {
            return Created("", result);
        }

        if (OperacaoValida())
        {
            return Accepted(new ViewModel<T>
            (
                notificationError: Notifications.GetNotifications()
            ));
        }

        return BadRequest(new ViewModel<T>
        (
            path: Request.Path.Value,
            remoteAddress: HttpContext?.Connection.RemoteIpAddress?.ToString(),
            notifications: Notifications.GetNotifications()
        ));
    }

    protected IActionResult Response<T>(T result) where T : IComparable => ResponseGeneric(result);

    protected IActionResult Response<T>(IEnumerable<T> result) where T : IComparable => ResponseGeneric(result);

    protected IActionResult Response(IEnumerable<dynamic> result) => ResponseGeneric(result);

}