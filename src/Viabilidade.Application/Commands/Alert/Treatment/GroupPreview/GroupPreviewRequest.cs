using MediatR;
using Viabilidade.Domain.DTO.Treatment;

namespace Viabilidade.Application.Commands.Alert.Treatment.GroupPreview
{
    public class GroupPreviewRequest : IRequest<TreatmentGroupPreviewDto>
    {
        public int Id { get; private set; }
        public int RegraAlertaEntidadeId { get; private set; }

        public GroupPreviewRequest(int id, int regraAlertaEntidadeId)
        {
            Id = id;
            RegraAlertaEntidadeId = regraAlertaEntidadeId;
        }

    }
}