using MediatR;
using Viabilidade.Domain.Models.Views;

namespace Viabilidade.Application.Commands.Org.Bond.Get
{
    public class GetBondRequest : IRequest<IEnumerable<BondModel>>
    {
        public string Search { get; private set; }
        public int SegmentId { get; private set; }

        public GetBondRequest(string search,  int segmentId)
        {
            Search = search;
            SegmentId = segmentId;
        }

    }
}