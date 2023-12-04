using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Interfaces.Services.UseCases.Rule;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.UseCases.Rule
{

    public class UpdateRuleService : IUpdateRuleService
    {
        private readonly IRuleService _ruleService;
        private readonly ICreateRuleService _createRuleService;
        private readonly IRuleActiveService _ruleActiveService;
        
        private IList<TagAlertModel> _tags;
        private IList<EntityRuleModel> _entityRules;


        public UpdateRuleService(IRuleService ruleService, ICreateRuleService createRuleService, IRuleActiveService ruleActiveService)
        {
            _ruleService = ruleService;
            _createRuleService = createRuleService;
            _ruleActiveService = ruleActiveService;
            _tags = new List<TagAlertModel>();
            _entityRules = new List<EntityRuleModel>();
        }

        public async Task<RuleModel> UpdateAsync(int ruleId, RuleModel rule)
        {

            await InactiveOldRuleAsync(ruleId);
            return await _createRuleService.CreateAsync(rule);

        }

        private async Task InactiveOldRuleAsync(int ruleId)
        {
            await _ruleActiveService.DeleteAsync(ruleId);
        }
    }
}

