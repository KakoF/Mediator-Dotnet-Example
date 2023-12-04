using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Viabilidade.Domain.Interfaces.File;
using Viabilidade.Domain.Interfaces.Notifications;
using Viabilidade.Domain.Interfaces.Services.Alert;
using Viabilidade.Domain.Interfaces.Services.Host;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Interfaces.Services.UseCases.Rule;
using Viabilidade.Domain.Notifications;
using Viabilidade.Service.Services.Alert;
using Viabilidade.Service.Services.File;
using Viabilidade.Service.Services.Host;
using Viabilidade.Service.Services.Org;
using Viabilidade.Service.Services.UseCases.Rule;

namespace Viabilidade.Service
{
    public static class RegisterService
    {
        public static void ConfigureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(_ =>
            {
                return new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage"));
            });
            services.AddScoped<IHostService, HostService>();
            services.AddScoped<INotificationHandler<Notification>, NotificationHandler>();
            services.AddScoped<IRuleService, RuleService>();
            services.AddScoped<IAlertService, AlertService>();
            services.AddScoped<IFavoriteAlertService, FavoriteAlertService>();
            services.AddScoped<IChannelService, ChannelService>();
            services.AddScoped<IEntityService, EntityService>();
            services.AddScoped<ISegmentService, SegmentService>();
            services.AddScoped<ISquadService, SquadService>();
            services.AddScoped<ISubgroupService, SubgroupService>();
            services.AddScoped<IAlgorithmService, AlgorithmService>();
            services.AddScoped<ITreatmentClassService, TreatmentClassService>();
            services.AddScoped<IOperatorService, OperatorService>();
            services.AddScoped<IParameterService, ParameterService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<ITreatmentTypeService, TreatmentTypeService>();
            services.AddScoped<IIndicatorFilterService, IndicatorFilterService>();
            services.AddScoped<IIndicatorService, IndicatorService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<ITagAlertService, TagAlertService>();
            services.AddScoped<ITreatmentService, TreatmentService>();
            services.AddScoped<IRChannelEntityRuleService, RChannelEntityRuleService>();
            services.AddScoped<IEntityRuleService, EntityRuleService>();
            services.AddScoped<IBondService, BondService>();
            services.AddScoped<ISilencedAlertService, SilencedAlertService>();
            services.AddScoped<IRTreatmentAlertService, RTreatmentAlertService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRSegmentEntityService, RSegmentEntityService>();
            services.AddScoped<IRuleActiveService, RuleActiveService>();
            services.AddScoped<ICreateRuleService, CreateRuleService>();
            services.AddScoped<IUpdateRuleService, UpdateRuleService>();
            

        }
    }
}
