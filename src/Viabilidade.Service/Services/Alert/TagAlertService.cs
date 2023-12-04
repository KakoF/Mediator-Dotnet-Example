using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.Alert
{
    public class TagAlertService : ITagAlertService
    {

        private readonly ITagAlertRepository _alertaTagRepository;
        private readonly IStorageCache _cache;
        public TagAlertService(ITagAlertRepository alertaTagRepository, IStorageCache cache)
        {
            _alertaTagRepository = alertaTagRepository;
            _cache = cache;
        }

        public async Task<TagAlertModel> CreateAsync(TagAlertModel model)
        {
            return await _alertaTagRepository.CreateAsync(model);
        }

        public async Task<bool> DeleteByRuleAsync(int ruleId)
        {
            return await _alertaTagRepository.DeleteByRuleAsync(ruleId);
        }

        public async Task<IEnumerable<TagAlertModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("TagAlert", () => _alertaTagRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<TagAlertModel> GetAsync(int id)
        {
            return await _alertaTagRepository.GetAsync(id);
        }

        public async Task<IEnumerable<TagAlertModel>> GetByRuleAsync(int ruleId, bool? active = null)
        {
            var models = await _cache.GetOrCreateAsync("TagAlert", () => _alertaTagRepository.GetAsync());
            if (active == null)
                return models.Where(x => x.RuleId == ruleId);
            return models.Where(x => x.RuleId == ruleId && x.Active == active);
        }

        public async Task<IEnumerable<TagAlertModel>> GetByTagAsync(int tagId, bool? active = null)
        {
            var models = await _cache.GetOrCreateAsync("TagAlert", () => _alertaTagRepository.GetAsync());
            if (active == null)
                return models.Where(x => x.TagId == tagId);
            return models.Where(x => x.TagId == tagId && x.Active == active);
        }



    }
}
