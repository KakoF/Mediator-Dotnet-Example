using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IStorageCache _cache;
        public TagService(ITagRepository tagRepository, IStorageCache cache)
        {
            _tagRepository = tagRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<TagModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Tag", () => _tagRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<TagModel> GetAsync(int id)
        {
            return await _tagRepository.GetAsync(id);
        }
    }
}
