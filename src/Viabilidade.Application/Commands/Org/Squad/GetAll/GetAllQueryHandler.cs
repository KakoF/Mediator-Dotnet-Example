using MediatR;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Application.Commands.Org.Squad.GetAll
{
    public class GetAllQueryHandler : IRequestHandler<GetActivesRequest<SquadModel>, IEnumerable<SquadModel>>
    {
        private readonly ISquadService _squadService;
        
        public GetAllQueryHandler(ISquadService squadService)
        {
            _squadService = squadService;
        }
        public async Task<IEnumerable<SquadModel>> Handle(GetActivesRequest<SquadModel> request, CancellationToken cancellationToken)
        {
            return await _squadService.GetAsync(request.Active);
            
        }
    }
}