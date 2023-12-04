using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Interfaces.Services.UseCases.Rule;
using Viabilidade.Domain.Models.Alert;

namespace Viabilidade.Service.Services.UseCases.Rule
{
    public class CreateRuleService : ICreateRuleService
    {

        private readonly IRuleService _ruleService;
        private readonly ITagAlertService _tagAlertService;
        private readonly IFavoriteAlertService _favoriteAlerteService;
        private readonly IParameterService _parameterService;
        private readonly IRChannelEntityRuleService _rChannelRuleEntityService;
        private readonly IEntityRuleService _entityRuleService;
        private readonly IRuleActiveService _ruleActiveService;

        private IList<TagAlertModel> _tags;
        private IList<EntityRuleModel> _entityRules;


        public CreateRuleService(IRuleService ruleService, ITagAlertService tagAlertService, IFavoriteAlertService favoriteAlerteService, IParameterService parameterService, IRChannelEntityRuleService rChannelRuleEntityService, IEntityRuleService entityRuleService, IRuleActiveService ruleActiveService)
        {
            _ruleService = ruleService;
            _tagAlertService = tagAlertService;
            _favoriteAlerteService = favoriteAlerteService;
            _parameterService = parameterService;
            _rChannelRuleEntityService = rChannelRuleEntityService;
            _entityRuleService = entityRuleService;
            _ruleActiveService = ruleActiveService;
            _tags = new List<TagAlertModel>();
            _entityRules = new List<EntityRuleModel>();
        }

        public async Task<RuleModel> CreateAsync(RuleModel rule)
        {

            var parameter = await _parameterService.CreateAsync(rule.Parameter);
            rule.Parameter = parameter;
            rule = await RuleCreateAsync(parameter.Id, rule);

            await foreach (var tag in TagsCreateAsync(rule.Id, rule.TagAlerts))
                _tags.Add(tag);

            await foreach (var entityRule in EntityRulesCreateAsync(rule.Id, rule.EntityRules))
                _entityRules.Add(entityRule);

            rule.TagAlerts = _tags;
            rule.EntityRules = _entityRules;

            return rule;
        }


        private async IAsyncEnumerable<TagAlertModel> TagsCreateAsync(int ruleId, IEnumerable<TagAlertModel> tags)
        {
            foreach (var tag in tags)
            {
                tag.RuleId = ruleId;
                yield return await _tagAlertService.CreateAsync(tag);
            }
        }

        private async IAsyncEnumerable<EntityRuleModel> EntityRulesCreateAsync(int ruleId, IEnumerable<EntityRuleModel> entityRules)
        {
            foreach (var entityRule in entityRules)
            {
                ParameterModel parameter = null;
                if (entityRule.Parameter != null)
                    parameter = await _parameterService.CreateAsync(entityRule.Parameter);

                entityRule.RuleId = ruleId;
                entityRule.ParameterId = parameter?.Id;
                entityRule.Parameter = parameter;

                var createEntityrule = await _entityRuleService.CreateAsync(entityRule);

                if (entityRule.Channels?.Any() ?? false)
                    await _rChannelRuleEntityService.CreateAsync(new RChannelEntityRuleModel { EntityRuleId = createEntityrule.Id, ChannelId = entityRule.Channels.FirstOrDefault().Id });

                yield return createEntityrule;
            }
        }

        private async Task<RuleModel> RuleCreateAsync(int parameterId, RuleModel rule)
        {
            var datetimeNow = DateTime.Now;

            rule.ParameterId = parameterId;
            rule.LastUpdateDate = datetimeNow;

            rule = await _ruleService.CreateAsync(rule);
            await _ruleActiveService.CreateAsync(new RuleActiveModel() { RuleId = rule.Id, CreateDate = datetimeNow });
            if ((bool)rule.Pinned)
                await _favoriteAlerteService.FavoriteAsync(new FavoriteAlertModel() { RuleId = rule.Id });

            return rule;
        }
    }
}
