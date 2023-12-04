using MediatR;

namespace Viabilidade.Application.Commands.BaseRequests
{
    public class SetActiveInactiveRequest<BaseModel> : IRequest<BaseModel>
    {
        public SetActiveInactiveRequest(int id, bool active)
        {
            Id = id;
            Active = active;
        }

        public bool Active { get; set; }
        public int Id { get; private set; }
    }
}