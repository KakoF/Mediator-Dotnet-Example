using MediatR;
using Viabilidade.Domain.DTO.Entity;
using Viabilidade.Domain.Interfaces.Services.Org;

namespace Viabilidade.Application.Commands.Org.Entity.Validate
{
    public class ValidateQueryHandler : IRequestHandler<ValidateRequest, EntityValidateDto>
    {
        private readonly IEntityService _entidadeService;
        
        public ValidateQueryHandler(IEntityService entidadeService)
        {
            _entidadeService = entidadeService;
        }
        public async Task<EntityValidateDto> Handle(ValidateRequest request, CancellationToken cancellationToken)
        {
            return await _entidadeService.ValidateAsync(request.SegmentId, request.EntityIds);
        }
    }
}