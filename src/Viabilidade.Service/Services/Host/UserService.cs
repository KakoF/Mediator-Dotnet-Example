using Microsoft.Extensions.Logging;
using Viabilidade.Domain.Interfaces.Services.Host;

namespace Viabilidade.Service.Services.Host
{
    public class UserService : IUserService
    {
        private readonly IHostService _hostService;
        private readonly ILogger<UserService> _logger;
        public UserService(IHostService hostService, ILogger<UserService> logger)
        {
            _hostService = hostService;
            _logger = logger;
        }
        public async Task<string> GetUserNameAsync(Guid id)
        {
            try
            {
                var list = await _hostService.GetUsersAsync();
                return list.Users.Where(x => x.UsersId.Equals(id)).Select(x => x.UserName).FirstOrDefault() ?? id.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Não foi possível recuperar o nome do usuário {id}");
                return id.ToString();
            }
        }
    }
}
