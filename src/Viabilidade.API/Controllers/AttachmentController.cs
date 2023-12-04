using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Viabilidade.API.Helpers;
using Viabilidade.Application.Commands.Alert.Attachment.GetFile;

namespace Viabilidade.API.Controllers
{
    [ApiController]
    [Authorize("Atlas")]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorResponse))]
    public class AttachmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AttachmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Download/{id}")]
        [SwaggerOperation(Summary = "Realiza o Download Anexo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetAsync(int id)
        {
            var file = await _mediator.Send(new GetFileRequest(id));
            if (file == null)
                return NotFound();
            return new FileContentResult(file.Item2, "application/octet-stream")
            {
                FileDownloadName = $"{Guid.NewGuid()}{Path.GetExtension(file.Item1)}",
            };

        }
    }
}