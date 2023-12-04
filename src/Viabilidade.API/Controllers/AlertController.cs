using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.Alert.Alert.GetAll;
using Viabilidade.Application.Commands.Alert.Alert.Group;
using Viabilidade.Application.Commands.Alert.Alert.GetByEntityRule;
using Viabilidade.Application.Commands.Alert.Alert.UpdateResponsible;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.DTO.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Alert;
using Viabilidade.Application.Commands.Alert.Alert.Generate;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class AlertController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlertController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Alertas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlertModel>))]
        public async Task<IEnumerable<AlertModel>> GetAsync()
        {
            return await _mediator.Send(new GetAllRequest());
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o Alerta")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlertModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<AlertModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<AlertModel>(id));
        }

        [HttpGet]
        [Route("{id}/Preview")]
        [SwaggerOperation(Summary = "Recupera o Alerta em Preview")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlertModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(AlertModel))]
        public async Task<AlertModel> PreviewAsync(int id)
        {
            return await _mediator.Send(new PreviewRequest<AlertModel>(id));
        }

        [HttpGet]
        [Route("Group")]
        [SwaggerOperation(Summary = "Recupera Agrupamento de Lista de Alertas (Filtros e Paginação)")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationModel<AlertGroupDto>))]
        public async Task<PaginationModel<AlertGroupDto>> GroupAsync([FromQuery] AlertQueryParams queryParams)
        {
            return await _mediator.Send(new GroupRequest(queryParams));
        }

        [HttpGet]
        [Route("EntityRule/{entityRuleId}")]
        [SwaggerOperation(Summary = "Recupera Lista de Alertas, pela Regra Entidade")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlertModel>))]
        public async Task<IEnumerable<AlertModel>> GroupAsync(int entityRuleId, [FromQuery] bool? treated)
        {
            return await _mediator.Send(new GetByEntityRuleRequest(entityRuleId, treated));
        }

        [HttpPatch]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Atualiza Responsável do Alerta")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlertModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<AlertModel> PathUserAsync(int id, [FromBody] UpdateResponsibleRequest request)
        {
            return await _mediator.Send(new UpdateResponsibleRequest(id, request.UserId));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Gerar Alerta")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GenerateAsync([FromBody] GenerateRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

    }
}