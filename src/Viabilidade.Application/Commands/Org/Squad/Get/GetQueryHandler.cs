using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Squad.Get
{
    public class GetQueryHandler : IRequestHandler<GetRequest<SquadModel>, SquadModel>
    {
        private readonly ISquadService _squadService;
        
        public GetQueryHandler(ISquadService squadService)
        {
            _squadService = squadService;
        }
        public async Task<SquadModel> Handle(GetRequest<SquadModel> request, CancellationToken cancellationToken)
        {
            return await _squadService.GetAsync(request.Id);
            
        }
    }
}