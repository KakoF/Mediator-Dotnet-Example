using MediatR;
using Viabilidade.Domain.DTO.Treatment;

namespace Viabilidade.Application.Commands.Alert.Treatment.Preview
{
    public class PreviewRequest : IRequest<TreatmentPreviewDto>
    {
        public int Id { get; private set; }

        public PreviewRequest(int id)
        {
            Id = id;
        }
    }
}