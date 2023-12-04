using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Repositories.Org.Views;
using Viabilidade.Infrastructure.Cache;
using Viabilidade.Infrastructure.DataConnector;
using Viabilidade.Infrastructure.Interfaces.DataConnector;
using Viabilidade.Infrastructure.Repositories.Alertas;
using Viabilidade.Infrastructure.Repositories.Org;
using Viabilidade.Infrastructure.Repositories.Org.Views;

namespace Viabilidade.Infrastructure
{
    public static class RegisterService
    {
        public static void ConfigureInfraStructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDbConnector>(db => new SqlServerConnector(configuration.GetConnectionString("Viabilidade")));
            services.AddMemoryCache();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStorageCache, StorageMemoryCache>();
            services.AddScoped<IAlertRepository, AlertRepository>();
            services.AddScoped<IFavoriteAlertRepository, FavoriteAlertRepository>();
            services.AddScoped<IRuleRepository, RuleRepository>();
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IEntityRepository, EntityRepository>();
            services.AddScoped<ISegmentRepository, SegmentRepository>();
            services.AddScoped<ISquadRepository, SquadRepository>();
            services.AddScoped<ISubgroupRepository, SubgroupRepository>();
            services.AddScoped<IAlgorithmRepository, AlgorithmRepository>();
            services.AddScoped<ITreatmentClassRepository, TreatmentClassRepository>();
            services.AddScoped<IOperatorRepository, OperatorRepository>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITreatmentTypeRepository, TreatmentTypeRepository>();
            services.AddScoped<IIndicatorRepository, IndicatorRepository>();
            services.AddScoped<IIndicatorFilterRepository, IndicatorFilterRepository>();
            services.AddScoped<ITagAlertRepository, TagAlertRepository>();
            services.AddScoped<ITreatmentRepository, TreatmentRepository>();
            services.AddScoped<IRChannelEntityRuleRepository, RChannelEntityRuleRepository>();
            services.AddScoped<IEntityRuleRepository, EntityRuleRepository>();
            services.AddScoped<IVwChannelSquadEntityRepository, VwChannelSquadEntityRepository>();
            services.AddScoped<ISilencedAlertRepository, SilencedAlertRepository>();
            services.AddScoped<IRTreatmentAlertRepository, RTreatmentAlertRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IRSegmentEntityRepository, RSegmentEntityRepository>();
            services.AddScoped<IRuleActiveRepository, RuleActiveRepository>();
        }
    }
}
