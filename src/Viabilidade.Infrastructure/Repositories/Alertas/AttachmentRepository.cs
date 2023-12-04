using Dapper;
using Microsoft.AspNetCore.Http;
using Viabilidade.Domain.Models.Alert;
using Viabilidade.Domain.Interfaces.Repositories.Alert;
using Viabilidade.Infrastructure.ContextAccessor;
using Viabilidade.Infrastructure.Interfaces.DataConnector;

namespace Viabilidade.Infrastructure.Repositories.Alertas
{
    public class AttachmentRepository : UserContextAccessor, IAttachmentRepository
    {
        private readonly IDbConnector _connector;
        public AttachmentRepository(IDbConnector connector, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _connector = connector;
        }
        public async Task<AttachmentModel> CreateAsync(AttachmentModel entity)
        {
            entity.Id = await _connector.dbConnection.QuerySingleAsync<int>("INSERT INTO Alertas.AnexoTratativa" +
                "(TratativaId, CaminhoArquivo, DataUpload, UsuarioIdUpload, Ativo, NomeArquivo) OUTPUT Inserted.Id " +
                "VALUES" +
                "(@TratativaId, @CaminhoArquivo, @DataUpload, @UsuarioIdUpload, @Ativo, @NomeArquivo)"
                , new { TratativaId = entity.TreatmentId, CaminhoArquivo = entity.PathFile, DataUpload = entity.UploadDate, UsuarioIdUpload = _userId, Ativo = entity.Active, NomeArquivo = entity.FileName }, _connector.dbTransaction);
            return entity;
        }


        public async Task<AttachmentModel> GetAsync(int id)
        {
            return await _connector.dbConnection.QueryFirstOrDefaultAsync<AttachmentModel>("Select Id, TratativaId as TreatmentId, CaminhoArquivo as PathFile, DataUpload as UploadDate, UsuarioIdUpload as UserId, Ativo as Active, NomeArquivo as FileName from Alertas.AnexoTratativa where id = @id", new { id }, _connector.dbTransaction);
        }

        public async Task<IEnumerable<AttachmentModel>> GetByTreatmentAsync(int treatmentId)
        {
            return await _connector.dbConnection.QueryAsync<AttachmentModel>("Select Id, TratativaId as TreatmentId, CaminhoArquivo as PathFile, DataUpload as UploadDate, UsuarioIdUpload as UserId, Ativo as Active, NomeArquivo as FileName from Alertas.AnexoTratativa where TratativaId = @treatmentId", new { treatmentId }, _connector.dbTransaction);
        }


    }
}
