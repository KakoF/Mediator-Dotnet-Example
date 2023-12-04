using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.FavoriteAlert.Unfavorite
{
    public class UnFavoriteCommandHandler : IRequestHandler<UnFavoriteRequest, FavoriteAlertModel>
    {
        private readonly IFavoriteAlertService _alertaFavoritoService;
        public UnFavoriteCommandHandler(IFavoriteAlertService alertaFavoritoService)
        {
            _alertaFavoritoService = alertaFavoritoService;
        }
        public async Task<FavoriteAlertModel> Handle(UnFavoriteRequest request, CancellationToken cancellationToken)
        {
            return await _alertaFavoritoService.UnFavoriteAsync(request.Id);
        }
    }
}