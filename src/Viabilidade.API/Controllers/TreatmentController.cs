using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.Alert.Treatment.Create;
using Viabilidade.Application.Commands.Alert.Treatment.Group;
using Viabilidade.Application.Commands.Alert.Treatment.GetByEntityRuleGroup;
using Viabilidade.Application.Commands.Alert.Treatment.GroupPreview;
using Viabilidade.Application.Commands.Alert.Treatment.Preview;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.DTO.Treatment;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Treatment;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class TreatmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TreatmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera a Tratativa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TreatmentModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<TreatmentModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<TreatmentModel>(id));
        }

        [HttpGet]
        [Route("{id}/Preview")]
        [SwaggerOperation(Summary = "Recupera a Tratativa em Preview")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TreatmentPreviewDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(TreatmentPreviewDto))]
        public async Task<TreatmentPreviewDto> PreviewAsync(int id)
        {
            return await _mediator.Send(new PreviewRequest(id));
        }

        [HttpGet]
        [Route("Group")]
        [SwaggerOperation(Summary = "Recupera Agrupamento de Tratativas (Filtros e Paginação)")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationModel<TreatmentGroupDto>))]
        public async Task<PaginationModel<TreatmentGroupDto>> GroupAsync([FromQuery] TreatmentQueryParams queryParams)
        {
            return await _mediator.Send(new GroupRequest(queryParams));
        }

        [HttpGet]
        [Route("EntityRule/{entityRuleId}")]
        [SwaggerOperation(Summary = "Recupera Agrupamento de Tratativas, pela Regra Entidade")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TreatmentByEntityRuleGroupDto>))]
        public async Task<IEnumerable<TreatmentByEntityRuleGroupDto>> GroupAsync(int entityRuleId)
        {
            return await _mediator.Send(new GetByEntityRuleGroupRequest(entityRuleId));
        }

        [HttpGet]
        [Route("{id}/{entityRuleId}/Preview")]
        [SwaggerOperation(Summary = "Recupera a Tratativa em Preview. Com Agrupamento de Informações")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TreatmentGroupPreviewDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(TreatmentGroupPreviewDto))]
        public async Task<TreatmentGroupPreviewDto> GroupPreviewAsync(int id, int entityRuleId)
        {
            return await _mediator.Send(new GroupPreviewRequest(id, entityRuleId));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cria Nova Tratativa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TreatmentModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<TreatmentModel> PostAsync([FromForm] CreateTreatmentRequest request)
        {
            return await _mediator.Send(request);
        }

    }
}