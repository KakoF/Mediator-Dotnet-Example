using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Viabilidade.Domain.Interfaces.Notifications;
using Viabilidade.Domain.Notifications;

namespace Viabilidade.API.Helpers.Filters
{
    public class NotificationFilter : IAsyncResultFilter
    {
        private readonly INotificationHandler<Notification> _notification;
        public NotificationFilter(INotificationHandler<Notification> notification)
        {
            _notification = notification;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {

            if (context.ModelState.IsValid && _notification.HasNotification())
            {
                context.HttpContext.Response.StatusCode = _notification.GetStatusCode();
                context.HttpContext.Response.ContentType = "application/json";

                var result = new ErrorResponse(_notification.GetStatusCode(), _notification.GetMessage(), _notification.GetNotificationsErrors());
                var notifications = JsonConvert.SerializeObject(result);
                await context.HttpContext.Response.WriteAsync(notifications);

                return;
            }

            await next();
        }

    }
}