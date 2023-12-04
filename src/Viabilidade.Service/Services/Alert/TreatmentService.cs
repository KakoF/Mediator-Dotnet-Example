using Viabilidade.Domain.DTO.Treatment;
using Viabilidade.Domain.Interfaces.Notifications;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Interfaces.Services.Host;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Pagination;
using Viabilidade.Domain.Models.QueryParams.Treatment;
using Viabilidade.Domain.Notifications;

namespace Viabilidade.Service.Services.Alert
{
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _alertaGeradoTratativaRepository;
        private readonly IRuleRepository _regraAlertaRepository;
        private readonly IEntityRuleRepository _regraEntidadeRepository;
        private readonly IParameterRepository _parametroRepository;
        private readonly IEntityRepository _entityRepository;
        private readonly IAlgorithmRepository _algorithmRepository;
        private readonly IOperatorRepository _operatorRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IUserService _userService;
        private readonly INotificationHandler<Notification> _notification;
        public TreatmentService(ITreatmentRepository alertaGeradoTratativaRepository, IRuleRepository regraAlertaRepository, IEntityRuleRepository regraEntidadeRepository, IParameterRepository parametroRepository, IEntityRepository entityRepository, IAlgorithmRepository algorithmRepository, IOperatorRepository operatorRepository, IAlertRepository alertRepository, IUserService userService, INotificationHandler<Notification> notification)
        {
            _alertaGeradoTratativaRepository = alertaGeradoTratativaRepository;
            _regraAlertaRepository = regraAlertaRepository;
            _regraEntidadeRepository = regraEntidadeRepository;
            _parametroRepository = parametroRepository;
            _entityRepository = entityRepository;
            _algorithmRepository = algorithmRepository;
            _operatorRepository = operatorRepository;
            _alertRepository = alertRepository;
            _userService = userService;
            _notification = notification;
        }
        public async Task<TreatmentModel> CreateAsync(TreatmentModel model)
        {
            return await _alertaGeradoTratativaRepository.CreateAsync(model);
        }
      
        public async Task<TreatmentModel> GetAsync(int id)
        {
            return await _alertaGeradoTratativaRepository.GetAsync(id);
        }

        public async Task<PaginationModel<TreatmentGroupDto>> GroupAsync(TreatmentQueryParams queryParams)
        {
            var data = await _alertaGeradoTratativaRepository.GroupAsync(queryParams);
            return new PaginationModel<TreatmentGroupDto>(data.Select(c => c.Item2).FirstOrDefault(), queryParams.Page, queryParams.TotalPage, data.Select(c => c.Item1));
        }

        public async Task<IEnumerable<TreatmentByEntityRuleGroupDto>> GetByEntityRuleGroupAsync(int entityRuleId)
        {
            var model = await _alertaGeradoTratativaRepository.GetByEntityRuleGroupAsync(entityRuleId);
            foreach (var item in model)
            {
                item.UserName = await _userService.GetUserNameAsync(item.UserId);
            }
            return model;
        }

        public async Task<TreatmentPreviewDto> PreviewAsync(int id)
        {
            var model = await _alertaGeradoTratativaRepository.PreviewDetailAsync(id);
            if (model == null)
            {
                _notification.AddNotification(404, "Tratativa não encontrada");
                return null;
            }

            model.UserName = await _userService.GetUserNameAsync((Guid)model.UserId);
            var regraAlerta = await _regraAlertaRepository.PreviewAsync((int)model.Alerts.FirstOrDefault().EntityRule.RuleId);
            var entity = await _entityRepository.GetByIdAsync((int)model.Alerts.LastOrDefault().EntityId);
            return new TreatmentPreviewDto()
            {
                Treatment = model,
                Rule = regraAlerta,
                Entity = entity
            };
        }

        public async Task<TreatmentGroupPreviewDto> GroupPreviewAsync(int id, int entityRuleId)
        {
            var entity = await _alertaGeradoTratativaRepository.PreviewAsync(id);
            
            if (entity == null)
            {
                _notification.AddNotification(404, "Tratativa não encontrada");
                return null;
            }
            var group = await _alertaGeradoTratativaRepository.GetByEntityRuleGroupAsync(entityRuleId);

            var lastAlert = await _alertRepository.GetLastByEntityRuleAsync(entityRuleId);
            var alert = await _alertRepository.PreviewAsync(lastAlert.Id);
            
            var regraEntidade = await _regraEntidadeRepository.PreviewAsync(entityRuleId);
            regraEntidade.Entity = await _entityRepository.GetByIdAsync((int)regraEntidade.EntityId);
            regraEntidade.Rule.Algorithm = await _algorithmRepository.GetAsync((int)regraEntidade.Rule.AlgorithmId);
            regraEntidade.Rule.Operator = await _operatorRepository.GetAsync((int)regraEntidade.Rule.OperatorId);

            if (regraEntidade.ParameterId == null || regraEntidade.Parameter.Active == false)
                regraEntidade.Parameter = await _parametroRepository.GetAsync((int)regraEntidade.Rule.ParameterId);

            return new TreatmentGroupPreviewDto()
            {
                Alert = alert,
                Treatment = entity,
                ClassCalc = new TreatmentCalc().CalcularClasses(group),
                TreatmentsQuantity = group.Count(),
                EntityRule = regraEntidade,
                Parameter = regraEntidade.Parameter
            };
        }
    }
}
