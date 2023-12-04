using FluentValidation;
using Viabilidade.Application.Commands.Alert.Rule.Create;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Repositories.Org;

namespace Viabilidade.Application.Commands.Alert.Rule.Update.Validators
{
    public class UpdateRuleValidator : AbstractValidator<UpdateRuleRequest>
    {
        private readonly IIndicatorRepository _indicadorRepository;
        private readonly IOperatorRepository _operadorRepository;
        private readonly IAlgorithmRepository _algoritmoTipoRepository;
        private readonly IEntityRepository _entidadeRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IRSegmentEntityRepository _rSegmentEntityRepository;
        public UpdateRuleValidator(IIndicatorRepository indicadorRepository, IOperatorRepository operadorRepository, IAlgorithmRepository algoritmoTipoRepository, IEntityRepository entidadeRepository, ITagRepository tagRepository, IRSegmentEntityRepository rSegmentEntityRepository)
        {
            _indicadorRepository = indicadorRepository;
            _operadorRepository = operadorRepository;
            _algoritmoTipoRepository = algoritmoTipoRepository;
            _entidadeRepository = entidadeRepository;
            _tagRepository = tagRepository;
            _rSegmentEntityRepository = rSegmentEntityRepository;

            RuleFor(a => a.Name)
               .NotEmpty().WithMessage("Nome não pode ser vazio")
               .MaximumLength(255).WithMessage("Nome tem limite máximo de de 255 caracteres");

            RuleFor(a => a.Description)
              .MaximumLength(255).WithMessage("Descrição tem limite máximo de de 255 caracteres");

            RuleFor(a => a.AlgorithmId)
                .NotEmpty().WithMessage("Algoritimo não pode ser vazio")
                .MustAsync(async (value, c) => await AlgorithmExistAsync(value)).WithMessage((value) => $"Tipo {value.AlgorithmId} não encontrado");

            RuleFor(a => a.IndicatorId)
                .NotEmpty().WithMessage("Indicador não pode ser vazio")
                .MustAsync(async (value, c) => await IndicatorExistAsync(value)).WithMessage((value) => $"Indicador {value.IndicatorId} não encontrado");

            RuleFor(a => a.OperatorId)
                .NotEmpty().WithMessage("Operador não pode ser vazio")
                .MustAsync(async (value, c) => await OperatorExistAsync(value)).WithMessage((value) => $"Operador {value.OperatorId} não encontrado");

            RuleFor(a => a.Active)
                .NotEmpty().WithMessage("Ativo não pode ser vazio");

            RuleFor(a => a.Pinned)
                .NotNull().WithMessage("Favorito não pode ser vazio");

            RuleFor(a => a.Parameter)
                .NotEmpty().WithMessage("Parametro não pode ser vazio");

            RuleFor(a => a.Tags)
               .Must(x => x.Select(c => c.Id).Distinct().Count() == x.Count()).WithMessage("Existem tags duplicadas")
               .MustAsync(async (value, c) => await TagExistAsync(value)).WithMessage("Tag não encontrada");

            RuleFor(a => a.EntityRules)
               .NotEmpty().WithMessage("Vínculo de squads/entidades/canais não pode ser vazio");

            RuleFor(a => a)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var indicator = await _indicadorRepository.GetAsync(value.IndicatorId);
                    foreach (var entities in value.EntityRules)
                    {
                        var entidade = await _entidadeRepository.GetAsync((int)entities.EntityId);
                        var segmentoEntidade = await _rSegmentEntityRepository.GetBySegmentEntityAsync(indicator.SegmentId, (int)entities.EntityId);

                        if (entidade == null || segmentoEntidade == null)
                        {
                            if (entidade == null)
                                context.AddFailure($"Entidade {entities.EntityId} não encontrada");
                            else
                               if (indicator != null)
                                context.AddFailure($"Entidade {entities.EntityId} não pertence ao segmento do indicador {indicator?.Description}");
                        }
                    }
                });
            _rSegmentEntityRepository = rSegmentEntityRepository;
        }

        private async Task<bool> AlgorithmExistAsync(int id)
        {
            var tipo = await _algoritmoTipoRepository.GetAsync(id);
            if (tipo == null || !tipo.Active)
                return false;
            return true;
        }
        private async Task<bool> IndicatorExistAsync(int id)
        {
            var indicador = await _indicadorRepository.GetAsync(id);
            if (indicador == null || !indicador.Active)
                return false;
            return true;
        }

        private async Task<bool> OperatorExistAsync(int id)
        {
            var operador = await _operadorRepository.GetAsync(id);
            if (operador == null || !operador.Active)
                return false;
            return true;
        }

        private async Task<bool> TagExistAsync(IEnumerable<CreateTagsRequest> tags)
        {
            foreach (var t in tags)
            {
                var tag = await _tagRepository.GetAsync(t.Id);
                if (tag == null || !tag.Active)
                    return false;
            }
            return true;
        }
    }
}

