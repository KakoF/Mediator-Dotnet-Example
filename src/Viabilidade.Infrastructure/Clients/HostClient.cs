using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Viabilidade.Domain.Exceptions;
using Viabilidade.Domain.Interfaces.Client;
using Viabilidade.Domain.Interfaces.Notifications;
using Viabilidade.Domain.Models.Client;
using Viabilidade.Domain.Models.Client.Abstractions;
using Viabilidade.Domain.Notifications;

namespace Viabilidade.Infrastructure.Clients
{
    public class HostClient : ClientFactoryAbstract<object, ClientResponseModel>, IHostClient
    {
        //private readonly ILogger<HostClient> _logger;
        private readonly IHttpContextAccessor _accessor;

        public HostClient(ILogger<HostClient> logger, IHttpContextAccessor acessor, HttpClient httpClient, INotificationHandler<Notification> notification) : base(logger, httpClient, notification)
        {
            //_logger = logger;
            _accessor = acessor;

            /*bool needToValidateSessionOnAltasHost = true;
            string[] pathValidationExclusion = { "health", "index.html", "favicon.ico", "swagger", "oidc-callback" };

            string encodedUrl = _accessor.HttpContext.Request.Path.ToString().ToLower();
            needToValidateSessionOnAltasHost = !pathValidationExclusion.Any(encodedUrl.Contains);

            _logger.LogInformation($"Path {encodedUrl} need to validate session on Altas Host: {needToValidateSessionOnAltasHost} ");

            if (!needToValidateSessionOnAltasHost)
                return;*/
            if (!_accessor.HttpContext.Request.Headers["SESSID"].Any() || !_accessor.HttpContext.Request.Headers["User-Agent"].Any())
                throw new DomainException("Unauthorized", 401);

            httpClient.DefaultRequestHeaders.Add("SESSID", _accessor.HttpContext.Request.Headers["SESSID"].ToString());
            httpClient.DefaultRequestHeaders.Add("User-Agent", _accessor.HttpContext.Request.Headers["User-Agent"].ToString());
        }
    }
}
