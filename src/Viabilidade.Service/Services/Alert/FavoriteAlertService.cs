using Viabilidade.Domain.Interfaces.Notifications;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Notifications;

namespace Viabilidade.Service.Services.Alert
{
    public class FavoriteAlertService : IFavoriteAlertService
    {
        private readonly IFavoriteAlertRepository _alertaFavoritoRepository;
        private readonly IRuleRepository _regraRepository;
        private readonly INotificationHandler<Notification> _notification;
        public FavoriteAlertService(IFavoriteAlertRepository alertaFavoritoRepository, IRuleRepository regraRepository, INotificationHandler<Notification> notification)
        {
            _alertaFavoritoRepository = alertaFavoritoRepository;
            _regraRepository = regraRepository;
            _notification = notification;
        }

        public async Task<FavoriteAlertModel> FavoriteAsync(FavoriteAlertModel model)
        {
            await ValidateFavorite((int)model.RuleId);
            if(_notification.HasNotification())
                return null;

            model.UpdateDate = DateTime.Now;
            model.Active = true;
            return await _alertaFavoritoRepository.CreateAsync(model);
        }

        private async Task ValidateFavorite(int ruleId)
        {
            if (await _regraRepository.GetAsync(ruleId) == null)
                _notification.AddNotification(404, "Regra não encontrada");
            if (await _alertaFavoritoRepository.ExistFavoriteAsync(ruleId))
                _notification.AddNotification(400, "Regra já está favoritada");
        }

        public async Task<FavoriteAlertModel> GetAsync(int id)
        {
            return await _alertaFavoritoRepository.GetAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _alertaFavoritoRepository.DeleteAsync(id);
        }

        public async Task<FavoriteAlertModel> UnFavoriteAsync(int ruleId)
        {
            var favorito = await _alertaFavoritoRepository.GetByRuleUserAsync(ruleId);
            if (favorito == null)
            {
                _notification.AddNotification(403, "Alteração não permitida");
                return null;
            }

            favorito.UpdateDate = DateTime.Now;
            favorito.Active = false;
            return await _alertaFavoritoRepository.UpdateAsync(favorito.Id, favorito);

        }
    }
}
