using FluentValidation;
using Viabilidade.Application.Commands.Alert.Treatment.Create;
using Viabilidade.Domain.Interfaces.Repositories.Alert;

namespace Viabilidade.Application.Commands.Alert.Rule.Create.Validators
{
    public class CreateTreatmentValidator : AbstractValidator<CreateTreatmentRequest>
    {
        private readonly IAlertRepository _alertaGeradoRepository;
        private readonly ITreatmentClassRepository _classeTratativaRepository;
        private readonly ITreatmentTypeRepository _tipoTratativaRepository;
        
        public CreateTreatmentValidator(IAlertRepository alertaGeradoRepository, ITreatmentClassRepository classeTratativaRepository, ITreatmentTypeRepository tipoTratativaRepository)
        {
            _alertaGeradoRepository = alertaGeradoRepository;
            _classeTratativaRepository = classeTratativaRepository;
            _tipoTratativaRepository = tipoTratativaRepository;
           

            RuleFor(a => a.AlertIds)
              .NotEmpty().WithMessage("Alerta não pode ser vazio")
              .Must(x => x.Select(c => c).Distinct().Count() == x.Count()).WithMessage("Existem alertas duplicados")
              .ForEach(vinculos =>
              {
                  vinculos.MustAsync(async (value, c) => await AlertsExistAsync(value))
                 .WithMessage((value, c) => $"Alerta {c} não encontrado");
              });

            RuleFor(a => a.TreatmentClassId)
              .NotEmpty().WithMessage("Classe tratativa não pode ser vazia")
              .MustAsync(async (value, c) => await TreatmentExistAsync(value)).WithMessage("Classe tratativa não encontrada");

            RuleFor(a => a.TreatmentTypeId)
              .NotEmpty().WithMessage("Tipo tratativa não pode ser vazia")
              .MustAsync(async (value, c) => await TreatmentTypeExistAsync(value)).WithMessage("Tipo tratativa não encontrada");

            RuleFor(a => a.Description)
              .NotEmpty().WithMessage("Descrição não pode ser vazia")
              .MaximumLength(4000).WithMessage("Descrição tem limite máximo de de 4000 caracteres");

            RuleFor(a => a)
              .Must(DaysSilenced).WithMessage("Dias silenciado não poder ser vazio");

        }

        private async Task<bool> AlertsExistAsync(int alertaId)
        {
            var alerta = await _alertaGeradoRepository.GetAsync(alertaId);
            if (alerta == null || alerta.Active == false || alerta.Treated == true)
                return false;
            return true;
        }

        private async Task<bool> TreatmentExistAsync(int tratativaId)
        {
            var tratativa = await _classeTratativaRepository.GetAsync(tratativaId);
            if(tratativa == null || !tratativa.Active)
                return false;

            return true;
        }

        private async Task<bool> TreatmentTypeExistAsync(int tipoTratativaId)
        {
            var tipoTratativa = await _tipoTratativaRepository.GetAsync(tipoTratativaId);
            if (tipoTratativa == null || !tipoTratativa.Active)
                return false;

            return true;
        }

        private bool DaysSilenced(CreateTreatmentRequest tratativa)
        {
            if (tratativa.Mute == true && tratativa.MuteDays == null)
                return false;
            return true;
        }


    }
}
