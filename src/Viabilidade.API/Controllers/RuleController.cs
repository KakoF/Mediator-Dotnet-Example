using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.Alert.Alert.Generate;
using Viabilidade.Application.Commands.Alert.FavoriteAlert.Favorite;
using Viabilidade.Application.Commands.Alert.FavoriteAlert.Unfavorite;
using Viabilidade.Application.Commands.Alert.Rule.Create;
using Viabilidade.Application.Commands.Alert.Rule.GetAll;
using Viabilidade.Application.Commands.Alert.Rule.Group;
using Viabilidade.Application.Commands.Alert.Rule.SetRootRuleToRules;
using Viabilidade.Application.Commands.Alert.Rule.Update;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.DTO.Rule;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Rule;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class RuleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RuleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Regras")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RuleModel>))]
        public async Task<IEnumerable<RuleModel>> GetAsync()
        {
            return await _mediator.Send(new GetAllRequest());
        }

        [HttpGet]
        [Route("{id}/Preview")]
        [SwaggerOperation(Summary = "Recupera a Regra em Preview")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RuleModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RuleModel))]
        public async Task<RuleModel> PreviewAsync(int id)
        {
            return await _mediator.Send(new PreviewRequest<RuleModel>(id));
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Cria Nova Regra")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RuleModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<RuleModel> PostAsync([FromBody] CreateRuleRequest request)
        {
            return await _mediator.Send(request);
        }

        [HttpGet]
        [Route("Group")]
        [SwaggerOperation(Summary = "Recupera Agrupamento de Regras (Filtros e Paginação)")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationModel<RuleGroupDto>))]
        public async Task<PaginationModel<RuleGroupDto>> GroupAsync([FromQuery] RuleQueryParams queryParams)
        {
            return await _mediator.Send(new GroupRequest(queryParams));
        }

        [HttpPut]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Altera a Regra")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RuleModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<RuleModel> PutAsync(int id, [FromBody] UpdateRuleRequest request)
        {
            return await _mediator.Send(new UpdateRuleRequest(id, request.Name, request.Description, request.AlgorithmId, request.IndicatorId, request.OperatorId, request.Active, request.Pinned, request.Parameter, request.Tags, request.EntityRules));
        }

        [HttpPatch]
        [Route("{id}/Active")]
        [SwaggerOperation(Summary = "Ativa a Regra")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RuleModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<RuleModel> ActiveAsync(int id)
        {
            return await _mediator.Send(new SetActiveInactiveRequest<RuleModel>(id, true));
        }

        [HttpPatch]
        [Route("{id}/Inactive")]
        [SwaggerOperation(Summary = "Desativa a Regra")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RuleModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<RuleModel> InactiveAsync(int id)
        {
            return await _mediator.Send(new SetActiveInactiveRequest<RuleModel>(id, false));
        }


        [HttpPatch]
        [Route("{id}/Favorite")]
        [SwaggerOperation(Summary = "Favorita a Regra")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FavoriteAlertModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<FavoriteAlertModel> FavoriteAsync(int id)
        {
            return await _mediator.Send(new FavoriteRequest(id));
        }

        [HttpDelete]
        [Route("{id}/UnFavorite")]
        [SwaggerOperation(Summary = "Desfavorita a Regra")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(FavoriteAlertModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<FavoriteAlertModel> UnFavoriteAsync(int id)
        {
            return await _mediator.Send(new UnFavoriteRequest(id));
        }

        [HttpGet]
        [Route("SetRootRuleToRules")]
        [SwaggerOperation(Summary = "Adicionar Identificador de Regra Raiz para as Regras que não possuem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> SetRootRuleToRulesAsync()
        {
            await _mediator.Send(new SetRootRuleToRulesRequest());
            return Ok();
        }
    }
}