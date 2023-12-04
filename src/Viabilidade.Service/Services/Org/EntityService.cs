using Viabilidade.Domain.DTO.Entity;
using Viabilidade.Domain.Interfaces.Cache;
using Viabilidade.Domain.Interfaces.Repositories.Org;
using Viabilidade.Domain.Interfaces.Services.Org;
using Viabilidade.Domain.Models.Org;

namespace Viabilidade.Service.Services.Org
{
    public class EntityService : IEntityService
    {
        private readonly IEntityRepository _entidadeRepository;
        private readonly IStorageCache _cache;
        public EntityService(IEntityRepository entidadeRepository, IStorageCache cache)
        {
            _entidadeRepository = entidadeRepository;
            _cache = cache;
        }

        public async Task<IEnumerable<EntityModel>> GetAllFilter(int? id, string name, string originalEntityId)
        {
            return await _entidadeRepository.GetAllFilter(id, name, originalEntityId);
        }

        public async Task<IEnumerable<EntityModel>> GetBySegmentSquadAsync(int squadId, int segmentId)
        {
            return await _entidadeRepository.GetBySegmentSquadAsync(squadId, segmentId);
        }

        public async Task<IEnumerable<EntityModel>> GetAsync(bool? active)
        {
            var models = await _cache.GetOrCreateAsync("Entity", () => _entidadeRepository.GetAsync());
            if (active == null)
                return models;
            return models.Where(x => x.Active == active);
        }

        public async Task<EntityModel> GetAsync(int id)
        {
            return await _entidadeRepository.GetAsync(id);
        }

        public async Task<EntityModel> GetByOriginalEntityAsync(int originalEntityId)
        {
            return await _entidadeRepository.GetByOriginalEntityAsync(originalEntityId);
        }

        public Task<IEnumerable<EntityModel>> GetBySquadsAsync(IEnumerable<int> squadIds)
        {
            return _entidadeRepository.GetBySquadsAsync(squadIds);
        }

        public async Task<EntityValidateDto> ValidateAsync(int segmentId, IEnumerable<string> entityIds)
        {
            var data = new EntityValidateDto();

            var entidadesIdsInt = new List<int>();

            foreach (string entidadeId in entityIds)
            {
                bool intOutput = int.TryParse(entidadeId, out int x);

                if (intOutput)
                    entidadesIdsInt.Add(x);
                else
                    data.Invalids.Add(new InvalidEntityDTO(entidadeId, "Entidade inválida"));
            }

            IEnumerable<EntitySquadDto> bonds = await _entidadeRepository.GetByOriginalIdsAsync(entidadesIdsInt);

            IEnumerable<int> entitiesNotFound = entidadesIdsInt.Where(x => !bonds.Any(e => e.OriginalEntityId == x));

            foreach (int entidadeId in entitiesNotFound)
                data.Invalids.Add(new InvalidEntityDTO(entidadeId.ToString(), "Não existe"));


            IEnumerable<EntitySquadDto> entidadesHasSegment = bonds.Where(x => x.SegmentId == segmentId);

            IEnumerable<EntitySquadDto> entidadesHasNotSegment = bonds.Where(x => x.SegmentId != segmentId && !entidadesHasSegment.Any(y => y.OriginalEntityId == x.OriginalEntityId)).DistinctBy(x => x.EntityId);

            data.Valids = entidadesHasSegment.ToList();


            foreach (EntitySquadDto entidade in entidadesHasNotSegment)
            {
                if (entidade.SegmentId != segmentId)
                {
                    data.Invalids.Add(new InvalidEntityDTO(entidade.OriginalEntityId.ToString(), "Entidade pertence à outro segmento"));
                    continue;
                }                
            }

            return data;
        }

        public async Task<IEnumerable<EntitySquadDto>> GetBySquadIdsAsync(IEnumerable<int> squadIds)
        {
            return await _entidadeRepository.GetBySquadIdsAsync(squadIds);
        }
    }
}
