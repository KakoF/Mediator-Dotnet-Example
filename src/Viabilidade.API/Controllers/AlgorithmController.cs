using MediatR;
using Microsoft.AspNetCore.Mvc;
using Viabilidade.Application.Commands.BaseRequests;
using Viabilidade.Domain.Models.Alert;
using Microsoft.AspNetCore.Authorization;
using Viabilidade.API.Helpers;
using Swashbuckle.AspNetCore.Annotations;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class AlgorithmController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlgorithmController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Tipos de Algoritmo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<AlgorithmModel>))]
        public async Task<IEnumerable<AlgorithmModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<AlgorithmModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera o Tipo de Algoritmo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AlgorithmModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<AlgorithmModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<AlgorithmModel>(id));
        }
    }
}