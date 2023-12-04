using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Domain.Interfaces.Repositories.Alert
{
    public interface ITagAlertRepository : IBaseRepository<TagAlertModel>
    {
        Task<TagAlertModel> CreateAsync(TagAlertModel entity);
        Task<bool> DeleteByRuleAsync(int ruleId);
        Task<IEnumerable<TagAlertModel>> GetByRuleAsync(int ruleId, bool? active = null);
        Task<IEnumerable<TagAlertModel>> GetByTagAsync(int tagId, bool? active = null);
    }
}
