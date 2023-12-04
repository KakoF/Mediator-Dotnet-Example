using MediatR;
using System.Text.Json.Serialization;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Alert.UpdateResponsible
{
    public class UpdateResponsibleRequest : IRequest<AlertModel>
    {
        public UpdateResponsibleRequest(int id, Guid? userId)
        {
            Id = id;
            UserId = userId;
        }

        public Guid? UserId { get; set; }
        [JsonIgnore]
        public int Id { get;  private set; }
    }
}
