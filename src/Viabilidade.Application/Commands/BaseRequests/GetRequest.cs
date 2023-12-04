using MediatR;

namespace Viabilidade.Application.Commands.BaseRequests
{
    public class GetRequest<BaseModel> : IRequest<BaseModel>
    {
        public int Id { get; private set; }

        public GetRequest(int id)
        {
            this.Id = id;
        }

    }
}