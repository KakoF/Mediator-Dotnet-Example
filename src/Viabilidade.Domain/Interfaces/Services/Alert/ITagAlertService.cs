using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Services.Alert
{
    public interface ITagAlertService
    {
        Task<TagAlertModel> CreateAsync(TagAlertModel model);
        Task<IEnumerable<TagAlertModel>> GetAsync(bool? active);
        Task<bool> DeleteByRuleAsync(int ruleId);
        Task<IEnumerable<TagAlertModel>> GetByRuleAsync(int ruleId, bool? active = null);
        Task<IEnumerable<TagAlertModel>> GetByTagAsync(int tagId, bool? active = null);
    }
}
