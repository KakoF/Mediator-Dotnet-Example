using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.Alert.Indicator.GetBySegment;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class IndicatorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public IndicatorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Indicadores")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IndicatorModel>))]
        public async Task<IEnumerable<IndicatorModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<IndicatorModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o Indicador")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IndicatorModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IndicatorModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<IndicatorModel>(id));
        }

        [HttpGet]
        [Route("Segment/{segmentId}")]
        [SwaggerOperation(Summary = "Recupera Lista de Indicadores, pelo Segmento")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<IndicatorModel>))]
        public async Task<IEnumerable<IndicatorModel>> GetBySubGrupoAsync(int segmentId)
        {
            return await _mediator.Send(new GetBySegmentRequest(segmentId));
        }
    }
}