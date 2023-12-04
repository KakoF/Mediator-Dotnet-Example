using Viabilidade.Domain.DTO.Alert;
using Viabilidade.Domain.Interfaces.Notifications;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Alert;
using Viabilidade.Domain.Notifications;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Service.Services.Alert
{
    public class AlertService : IAlertService
    {
        private readonly IAlertRepository _alertaGeradoRepository;
        private readonly IRuleRepository _regraAlertaRepository;
        private readonly IEntityRepository _entityRepository;
        private readonly IEntityRuleRepository _entityRuleRepository;
        private readonly IRChannelEntityRuleRepository _rChannelEntityRuleRepository;
        private readonly INotificationHandler<Notification> _notification;

        public AlertService(IAlertRepository alertaGeradoRepository, IRuleRepository regraAlertaRepository, IEntityRepository entityRepository, IEntityRuleRepository entityRuleRepository, IRChannelEntityRuleRepository rChannelEntityRuleRepository, INotificationHandler<Notification> notification)
        {
            _alertaGeradoRepository = alertaGeradoRepository;
            _regraAlertaRepository = regraAlertaRepository;
            _entityRepository = entityRepository;
            _entityRuleRepository = entityRuleRepository;
            _rChannelEntityRuleRepository = rChannelEntityRuleRepository;
            _notification = notification;
        }
        public async Task<AlertModel> CreateAsync(AlertModel model)
        {
            return await _alertaGeradoRepository.CreateAsync(model);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _alertaGeradoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<AlertModel>> GetAsync()
        {
            return await _alertaGeradoRepository.GetAsync();
        }

        public async Task<AlertModel> GetAsync(int id)
        {
            return await _alertaGeradoRepository.GetAsync(id);
        }

        public async Task<IEnumerable<AlertModel>> GetAsync(IEnumerable<int> ids)
        {
            return await _alertaGeradoRepository.GetAsync(ids);
        }

        public async Task<PaginationModel<AlertGroupDto>> GroupAsync(AlertQueryParams queryParams)
        {
            var data = await _alertaGeradoRepository.GroupAsync(queryParams);
            return new PaginationModel<AlertGroupDto>(data.Select(c => c.Item2).FirstOrDefault(), queryParams.Page, queryParams.TotalPage, data.Select(c => c.Item1).ToList());
        }

        public async Task<AlertModel> UpdateAsync(int id, AlertModel model)
        {
            return await _alertaGeradoRepository.UpdateAsync(id, model);
        }

        public async Task<AlertModel> UpdateUserAsync(int id, AlertModel updateModel)
        {
            var alerta = await _alertaGeradoRepository.GetAsync(id);
            alerta.UserId = updateModel.UserId;
            return await _alertaGeradoRepository.UpdateAsync(id, alerta);
        }

        public async Task<AlertModel> PreviewAsync(int id)
        {
            var model = await _alertaGeradoRepository.PreviewAsync(id);
            if (model == null)
            {
                _notification.AddNotification(404, "Alerta não encontrado");
                return null;
            }
            model.Entity = await _entityRepository.GetByIdAsync((int)model.EntityId);
            model.EntityRule.Rule = await _regraAlertaRepository.PreviewAsync((int)model.EntityRule.RuleId);
            return model;
        }
        public async Task<IEnumerable<AlertModel>> GetByEntityRuleAsync(int entityRuleId, bool? treated)
        {
            return await _alertaGeradoRepository.GetByEntityRuleAsync(entityRuleId, treated);
        }

        public async Task GenerateAsync(int entityRuleId, int quantity)
        {

            try
            {

                var severities = new List<string>()
                {
                    "Baixa",
                    "Media",
                    "Alta",
                };


                var entityRule = await _entityRuleRepository.PreviewAsync(entityRuleId);
                var rule = await _regraAlertaRepository.PreviewAsync((int)entityRule.RuleId);
                var entity = await _entityRepository.GetByIdAsync((int)entityRule.EntityId);
                var entityRuleChannel = await _rChannelEntityRuleRepository.GetByEntityRuleAsync((int)entityRule.Id);

                Random randomGenerator = new Random(DateTime.Now.Millisecond);

                for (int i = 0; i < quantity; i++)
                {
                    Random r = new Random();
                    int index = r.Next(severities.Count);
                    string randomSeverity = severities[index];

                    var alert = new AlertModel()
                    {
                        EntityId = entity.OriginalEntityId,
                        EntityName = entityRule.Entity.Name,
                        EntityRuleId = entityRule.Id,
                        RuleName = rule.Name,
                        Version = DateTime.Now,
                        IndicatorFirstDate = DateTime.Now,
                        IndicatorLastDate = DateTime.Now,
                        Severity = randomSeverity,
                        Indicator = new decimal(randomGenerator.NextDouble()),
                        LowReferenceIndicator = new decimal(randomGenerator.NextDouble()),
                        MediumReferenceIndicator = new decimal(randomGenerator.NextDouble()),
                        HighReferenceIndicator = new decimal(randomGenerator.NextDouble()),
                        StatusId = 1,
                        FinishDate = DateTime.Now,
                        Active = true,
                        Treated = null,
                        IndicatorValue = null,
                        PercentageIndicator = null
                    };
                    await _alertaGeradoRepository.CreateAsync(alert);
                }
                await _alertaGeradoRepository.ExeuteProcAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
