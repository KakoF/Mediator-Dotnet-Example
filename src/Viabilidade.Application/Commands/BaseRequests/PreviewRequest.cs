using MediatR;

namespace Viabilidade.Application.Commands.BaseRequests
{
    public class PreviewRequest<BaseModel> : IRequest<BaseModel>
    {
        public int Id { get; private set; }

        public PreviewRequest(int id)
        {
            this.Id = id;
        }

    }
}