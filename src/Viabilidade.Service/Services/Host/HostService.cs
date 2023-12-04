using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Client;
using Viabilidade.Domain.Interfaces.Services.Host;
using Viabilidade.Domain.Models.Client.Host;

namespace Viabilidade.Service.Services.Host
{
    public class HostService : IHostService
    {
        private readonly IHostClient _hostClient;
        private readonly IConfiguration _config;
        private readonly IStorageCache _cache;
        private readonly string _productId;

        public HostService(IHostClient hostClient, IConfiguration config, IStorageCache cache)
        {
            _hostClient = hostClient;
            _config = config;
            _cache = cache;
            _productId = _config["ProductSettings:AnalyticsProductId"];
        }

        public async Task<UserInfoModel> GetUserAutenticateAsync()
        {
            var response = await _hostClient.GetAsync("/User/info");
            if (response == null)
                return null;

            var model = JsonConvert.DeserializeObject<UserInfoModel>(response.Data.ToString());
            return model;
        }

        public async Task<UserInfoModel> GetUserCredentialsAsync()
        {
            var response = await _hostClient.GetAsync("/User/credentials");
            if (response == null)
                return null;

            var model = JsonConvert.DeserializeObject<UserInfoModel>(response.Data.ToString());
            return model;
        }

        public async Task<UserDetailModel> GetUserInfoAsync(Guid userId)
        {
            var response = await _hostClient.GetAsync($"/User/detail?userId={userId}");
            if (response == null)
                return null;
            
            var model = JsonConvert.DeserializeObject<UserDetailModel>(response.Data.ToString());
            return model;
        }

        public async Task<bool> ValidadeSessionAsync()
        {
            var response = await _hostClient.GetAsync("/Auth/validate-session");
            if (response == null || response.StatusCode != 200)
                return false;
            if (response.StatusCode == 200) 
                return true;

            return false;
        }

        public async Task<UserListDetailModel> GetUsersAsync()
        {
            var response = await _cache.GetOrCreateAsync("UserList", () => _hostClient.GetAsync($"/user/list?ProductsId={_productId}"), TimeSpan.FromHours(2));
            if (response == null)
                return null;

            var model = JsonConvert.DeserializeObject<IEnumerable<UserListDetailModel>>(response.Data.ToString());
            return model.FirstOrDefault();
        }
    }
}
