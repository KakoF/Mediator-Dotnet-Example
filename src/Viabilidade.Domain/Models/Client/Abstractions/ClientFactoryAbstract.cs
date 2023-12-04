using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using Viabilidade.Domain.Interfaces.Client.Factory;
using Viabilidade.Domain.Interfaces.Notifications;
using Viabilidade.Domain.Notifications;

namespace Viabilidade.Domain.Models.Client.Abstractions
{
    public class ClientFactoryAbstract<T, O> : IClientFactory<T, ClientResponseModel>
    {
        protected readonly ILogger<ClientFactoryAbstract<T, O>> _logger;
        protected readonly HttpClient _httpClient;
        private readonly INotificationHandler<Notification> _notification;

        public ClientFactoryAbstract(ILogger<ClientFactoryAbstract<T, O>> logger, HttpClient httpClient, INotificationHandler<Notification> notification)
        {
            _notification = notification;
            _httpClient = httpClient;
            _logger = logger;
        }

        public virtual async Task<ClientResponseModel> GetAsync(string path)
        {
            var clientResponse = await _httpClient.GetAsync(path);
            if(clientResponse.IsSuccessStatusCode)
            {
                return new ClientResponseModel((int) clientResponse.StatusCode, await clientResponse.Content.ReadAsStringAsync());
            }

            var error = await clientResponse.Content.ReadAsStringAsync();
            _notification.AddNotification((int)clientResponse.StatusCode, "Erro em consumir o recurso", $"Message {error}");
            _logger.LogError(error);
            return null;
        }

        public virtual async Task<ClientResponseModel> PostAsync(string path, T data)
        {
            var content = new StringContent(JsonConvert.SerializeObject(data), UnicodeEncoding.UTF8, "application/json");

            var clientResponse = await _httpClient.PostAsync(path, content);
            if (clientResponse.IsSuccessStatusCode)
            {
                return new ClientResponseModel((int)clientResponse.StatusCode, await clientResponse.Content.ReadAsStringAsync());
            }

            var error = await clientResponse.Content.ReadAsStringAsync();
            _notification.AddNotification((int)clientResponse.StatusCode, "Erro em consumir o recurso", $"Message {error}");
            _logger.LogError(error);
            return null;
        }
    }
}
