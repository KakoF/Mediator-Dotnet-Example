using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.Alert.EntityRule.GetByEntity;
using Viabilidade.Application.Commands.Alert.EntityRule.GetByRule;
using Viabilidade.Application.Commands.Alert.EntityRule.GroupByEntity;
using Viabilidade.Application.Commands.Alert.EntityRule.GroupByRule;
using Viabilidade.Domain.DTO.EntityRule;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams;
using Viabilidade.Domain.Models.QueryParams.EntityRule;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class EntityRuleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EntityRuleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Entity/{entityId}")]
        [SwaggerOperation(Summary = "Recupera Lista de Regras, pela Entidade")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntityRuleModel>))]
        public async Task<IEnumerable<EntityRuleModel>> GetByEntityAsync(int entityId)
        {
            return await _mediator.Send(new GetByEntityRequest(entityId));
        }

        [HttpGet]
        [Route("Rule/{ruleId}")]
        [SwaggerOperation(Summary = "Recupera Lista de Regras da Entidade, pela Regra")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EntityRuleModel>))]
        public async Task<IEnumerable<EntityRuleModel>> GetByRuleAsync(int ruleId)
        {
            return await _mediator.Send(new GetByRuleRequest(ruleId));
        }

        [HttpGet]
        [Route("Entity/{entityId}/Group")]
        [SwaggerOperation(Summary = "Recupera Lista Paginada de Regras da Entidade, pela Entidade.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationModel<EntityRuleGroupByEntityDto>))]
        public async Task<PaginationModel<EntityRuleGroupByEntityDto>> GetByEntityAsync(int entityId, [FromQuery] EntityRuleQueryParams queryParams)
        {
            return await _mediator.Send(new GroupByEntityRequest(entityId, queryParams));
        }

        [HttpGet]
        [Route("Rule/{ruleId}/Group")]
        [SwaggerOperation(Summary = "Recupera Lista Paginada de Regras da Entidade, pela Regra")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationModel<EntityRuleGroupByRuleDto>))]
        public async Task<PaginationModel<EntityRuleGroupByRuleDto>> GetByRuleAsync(int ruleId, [FromQuery] EntityRuleQueryParams queryParams)
        {
            return await _mediator.Send(new GroupByRuleRequest(ruleId, queryParams));
        }
    }
}