using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.Alert.TreatmentType.GetByTreatmentClass;
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
    public class TreatmentTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TreatmentTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Recupera Lista de Tipos Tratativa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TreatmentTypeModel>))]
        public async Task<IEnumerable<TreatmentTypeModel>> GetAsync([FromQuery] bool? active)
        {
            return await _mediator.Send(new GetActivesRequest<TreatmentTypeModel>(active));
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(Summary = "Recupera a Tipo Tratativa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TreatmentTypeModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<TreatmentTypeModel> GetAsync(int id)
        {
            return await _mediator.Send(new GetRequest<TreatmentTypeModel>(id));
        }

        [HttpGet]
        [Route("TreatmentClass/{treatmentClassId}")]
        [SwaggerOperation(Summary = "Recupera Lista de Tipo Tratativas, pela Classe Tratativa")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TreatmentTypeModel>))]
        public async Task<IEnumerable<TreatmentTypeModel>> GetByTreatmentClassAsync(int treatmentClassId)
        {
            return await _mediator.Send(new GetByTreatmentClassRequest(treatmentClassId));
        }
    }
}