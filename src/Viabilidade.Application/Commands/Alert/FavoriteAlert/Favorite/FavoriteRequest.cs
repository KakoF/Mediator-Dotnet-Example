using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.FavoriteAlert.Favorite
{
    public class FavoriteRequest : IRequest<FavoriteAlertModel>
    {
        public FavoriteRequest(int id)
        {
            Id = id;
        }

        public int Id { get;  private set; }
    }
}
