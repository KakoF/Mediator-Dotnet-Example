using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface IFavoriteAlertService
    {
        Task<FavoriteAlertModel> GetAsync(int id);
        Task<FavoriteAlertModel> FavoriteAsync(FavoriteAlertModel model);
        Task<bool> DeleteAsync(int Id);
        Task<FavoriteAlertModel> UnFavoriteAsync(int id);
    }
}
