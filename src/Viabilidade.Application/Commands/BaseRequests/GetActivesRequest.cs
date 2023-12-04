
using MediatR;

namespace Viabilidade.Application.Commands.BaseRequests
{
    public class GetActivesRequest<BaseModel> : IRequest<IEnumerable<BaseModel>>
    {
        public bool? Active { get; private set; }

        public GetActivesRequest(bool? active = null)
        {
            Active = active;
        }
    }
}
