using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Domain.Interfaces.Services.Org
{
    public interface ISquadService
    {
        Task<IEnumerable<SquadModel>> GetAsync(bool? active);
        Task<SquadModel> GetAsync(int id);
    }
}