using MediatR;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Application.Commands.Alert.Indicator.GetBySegment
{
    public class GetBySegmentRequest : IRequest<IEnumerable<IndicatorModel>>
    {
        public int SegmentId { get; private set; }

        public GetBySegmentRequest(int segmentId)
        {
            SegmentId = segmentId;
        }

    }
}