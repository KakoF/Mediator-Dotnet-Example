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
    public class TreatmentClassController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TreatmentClassController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Classes Tratativa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TreatmentClassModel>))]
        public async Task<IEnumerable<TreatmentClassModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<TreatmentClassModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera a Classe Tratativa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TreatmentClassModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<TreatmentClassModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<TreatmentClassModel>(id));
        }
    }
}