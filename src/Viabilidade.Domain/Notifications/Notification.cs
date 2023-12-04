namespace Viabilidade.Domain.Notifications
{
    public class Notification
    {
        public Notification(string message, object errors = null, int statusCode = 400)
        {
            Message = message;
            Errors = errors;
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
        public string Message { get; }
        public object Errors { get; }
    }
}
