using MediatR;
using Viabilidade.Domain.Exceptions;
using Viabilidade.Domain.Interfaces.File;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Models.Enums;
using Viabilidade.Domain.Models.File;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Application.Commands.Alert.Treatment.Create
{
    public class CreateTreatmentCommandHandler : IRequestHandler<CreateTreatmentRequest, TreatmentModel>
    {
        private readonly IAlertService _alertaGeradoService;
        private readonly ISilencedAlertService _alertaGeradoSilenciadoService;
        private readonly IEntityRuleService _regraEntidadeService;
        private readonly ITreatmentService _alertaGeradoTratativaService;
        private readonly IRTreatmentAlertService _rAlertaGeradoTratativaService;
        private readonly IFileService _fileService;
        private readonly IAttachmentService _anexoTratativaService;
        private readonly IUnitOfWork _unitOfWork;
        public CreateTreatmentCommandHandler(IAlertService alertaGeradoService, ISilencedAlertService alertaGeradoSilenciadoService, IEntityRuleService regraEntidadeService, ITreatmentService alertaGeradoTratativaService, IRTreatmentAlertService rAlertaGeradoTratativaService, IFileService fileService, IAttachmentService anexoTratativaService, IUnitOfWork unitOfWork)
        {
            _alertaGeradoService = alertaGeradoService;
            _alertaGeradoSilenciadoService = alertaGeradoSilenciadoService;
            _regraEntidadeService = regraEntidadeService;
            _alertaGeradoTratativaService = alertaGeradoTratativaService;
            _rAlertaGeradoTratativaService = rAlertaGeradoTratativaService;
            _fileService = fileService;
            _anexoTratativaService = anexoTratativaService;
            _unitOfWork = unitOfWork;
        }
        public async Task<TreatmentModel> Handle(CreateTreatmentRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var alertas = await RecuperarAlertasAsync(request.AlertIds);
                
                _unitOfWork.BeginTransaction();
                
                await AtualizarAlertasAsync(alertas, request.Mute, request.MuteDays, request.Disable);
                var tratativa = await CriarTratativa(request, alertas);
                await CriarAnexos(request, tratativa);
                
                _unitOfWork.CommitTransaction();
                return tratativa;
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }

        private async Task<IEnumerable<AlertModel>> RecuperarAlertasAsync(IEnumerable<int> alertasId)
        {
            var alertas = await _alertaGeradoService.GetAsync(alertasId);
            var regraAlertaEntidadeId = alertas.FirstOrDefault()?.EntityRuleId;
            if (alertas.Any(x => x.EntityRuleId != regraAlertaEntidadeId))
                throw new DomainException("Os alertas selecionados não fazem relação entre si");

            return alertas;
        }

        private async Task AtualizarAlertasAsync(IEnumerable<AlertModel> alertas, bool? silenciado = null, int? diasSilenciado = null, bool? desativado = null)
        {
            await TratarAlertasAsync(alertas);
            await SilenciarAlertasAsync(alertas, silenciado, diasSilenciado);
            await DesativarRegraAlertasAsync(alertas.Select(x => x.EntityRuleId).Distinct(), desativado);
        }

        private async Task TratarAlertasAsync(IEnumerable<AlertModel> alertas)
        {
            foreach (var alerta in alertas)
            {
                alerta.Treated = true;
                alerta.FinishDate = DateTime.Now;
                await _alertaGeradoService.UpdateAsync(alerta.Id, alerta);
            }
        }

        private async Task SilenciarAlertasAsync(IEnumerable<AlertModel> alertas, bool? silenciado, int? diasSilenciado)
        {
            if (silenciado == true)
            {
                var dataIni = DateTime.Now;
                var dataFim = dataIni.AddDays(Convert.ToInt32(diasSilenciado));

                foreach (var alerta in alertas)
                {
                    await _alertaGeradoSilenciadoService.CreateAsync(new SilencedAlertModel()
                    {
                        AlertId = alerta.Id,
                        Active = true,
                        StartDateSilence = dataIni,
                        EndDateSilence = dataFim,
                    });
                }
            }
        }

        private async Task DesativarRegraAlertasAsync(IEnumerable<int> regraEntidadeIds, bool? desativado)
        {
            if (desativado == true)
            {
                foreach (var regraEntidadeId in regraEntidadeIds)
                {
                    var regraEntidade = await _regraEntidadeService.GetAsync(regraEntidadeId);
                    regraEntidade.Active = !desativado;
                    await _regraEntidadeService.UpdateAsync(regraEntidade.Id, regraEntidade);
                }
            }
        }

        private async Task<TreatmentModel> CriarTratativa(CreateTreatmentRequest request, IEnumerable<AlertModel> alertas)
        {

            var alertaGeradoTratativa = new TreatmentModel()
            {
                Date = DateTime.Now,
                TreatmentClassId = request.TreatmentClassId,
                TreatmentTypeId = request.TreatmentTypeId,
                Description = request.Description,
                Active = true,
            };
            var tratativa = await _alertaGeradoTratativaService.CreateAsync(alertaGeradoTratativa);

            foreach (var alerta in alertas)
            {
                await _rAlertaGeradoTratativaService.CreateAsync(new RTreatmentAlertModel() { AlertId = alerta.Id, TreatmentId = tratativa.Id });
            }

            return tratativa;
        }

        private async Task CriarAnexos(CreateTreatmentRequest request, TreatmentModel alertaGeradoTratativa)
        {
            if (request.Attachments is not null)
            {
                var arquivo = new FileModel()
                {
                    Files = request.Attachments,
                    ObjetoId = alertaGeradoTratativa.Id.ToString(),
                    Tipo = eFileType.AlertaTratativa,
                    UsuarioId = alertaGeradoTratativa.UserId.ToString(),
                };

                var arquivosSalvos = await _fileService.UploadAsync(arquivo);

                foreach (var item in arquivosSalvos)
                {
                    await _anexoTratativaService.CreateAsync(new AttachmentModel()
                    {
                        TreatmentId = alertaGeradoTratativa.Id,
                        PathFile = item.Caminho,
                        FileName = item.Nome,
                        UploadDate = DateTime.Now,
                        UserId = alertaGeradoTratativa.UserId,
                        Active = true,
                    });
                }
            }
        }
    }
}