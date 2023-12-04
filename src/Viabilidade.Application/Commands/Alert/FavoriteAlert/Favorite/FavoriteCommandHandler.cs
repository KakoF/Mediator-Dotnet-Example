using MediatR;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.FavoriteAlert.Favorite
{
    public class FavoriteCommandHandler : IRequestHandler<FavoriteRequest, FavoriteAlertModel>
    {
        private readonly IFavoriteAlertService _alertaFavoritoService;
        public FavoriteCommandHandler(IFavoriteAlertService alertaFavoritoService)
        {
            _alertaFavoritoService = alertaFavoritoService;
        }
        public async Task<FavoriteAlertModel> Handle(FavoriteRequest request, CancellationToken cancellationToken)
        {
            return await _alertaFavoritoService.FavoriteAsync(new FavoriteAlertModel() { RuleId = request.Id });
        }
    }
}