using Viabilidade.Domain.Interfaces.Client.Factory;
using Viabilidade.Domain.Models.Client;

namespace Viabilidade.Domain.Interfaces.Client
{
    public interface IHostClient: IClientFactory<object, ClientResponseModel>
    {
    }
}
