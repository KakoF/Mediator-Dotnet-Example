using Viabilidade.Domain.Models.Client.Host;

namespace Viabilidade.Domain.Interfaces.Services.Host
{
    public interface IHostService
    {
        Task<UserInfoModel> GetUserAutenticateAsync();
        Task<UserInfoModel> GetUserCredentialsAsync();
        Task<UserDetailModel> GetUserInfoAsync(Guid userId);
        Task<bool> ValidadeSessionAsync();
        Task<UserListDetailModel> GetUsersAsync();
    }
}
