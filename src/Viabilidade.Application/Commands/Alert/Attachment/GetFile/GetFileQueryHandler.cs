using MediatR;
using Viabilidade.Domain.Interfaces.File;
using Viabilidade.Domain.Interfaces.Services.Alert;

namespace Viabilidade.Application.Commands.Alert.Attachment.GetFile
{
    public class GetFileQueryHandler : IRequestHandler<GetFileRequest, Tuple<string, byte[]>>
    {
        private readonly IFileService _fileService;
        private readonly IAttachmentService _attachmentService;

        public GetFileQueryHandler(IFileService fileService, IAttachmentService attachmentService)
        {
            _fileService = fileService;
            _attachmentService = attachmentService;
        }
        public async Task<Tuple<string, byte[]>> Handle(GetFileRequest request, CancellationToken cancellationToken)
        {
            var file = await _attachmentService.GetAsync(request.Id);
            var bytesFile = await _fileService.GetAsync(file.PathFile);
            return Tuple.Create(file.FileName, bytesFile);
        }
    }
}