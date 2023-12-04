using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.FavoriteAlert.Unfavorite
{
    public class UnFavoriteRequest : IRequest<FavoriteAlertModel>
    {
        public UnFavoriteRequest(int id)
        {
            Id = id;
        }

        public int Id { get;  private set; }
    }
}
