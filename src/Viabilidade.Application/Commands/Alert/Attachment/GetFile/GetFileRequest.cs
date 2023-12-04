using MediatR;

namespace Viabilidade.Application.Commands.Alert.Attachment.GetFile
{
    public class GetFileRequest : IRequest<Tuple<string, byte[]>>
    {
        public int Id { get; private set; }

        public GetFileRequest(int id)
        {
            Id = id;
        }

    }
}