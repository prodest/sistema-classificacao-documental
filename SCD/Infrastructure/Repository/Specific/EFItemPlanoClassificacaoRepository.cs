using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Persistence.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Infrastructure.Repository.Specific
{
    public class EFItemPlanoClassificacaoRepository : IItemPlanoClassificacaoRepository
    {
        protected DbSet<ItemPlanoClassificacao> _set;
        private IMapper _mapper;

        public EFItemPlanoClassificacaoRepository(DbSet<ItemPlanoClassificacao> set, IMapper mapper)
        {
            _set = set;
            _mapper = mapper;
        }

        public async Task<ItemPlanoClassificacaoModel> AddAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            ItemPlanoClassificacao itemPlanoClassificacao = _mapper.Map<ItemPlanoClassificacao>(itemPlanoClassificacaoModel);

            var entityEntry = await _set.AddAsync(itemPlanoClassificacao);

            itemPlanoClassificacaoModel = _mapper.Map<ItemPlanoClassificacaoModel>(entityEntry.Entity);

            return itemPlanoClassificacaoModel;
        }

        public async Task<ItemPlanoClassificacaoModel> SearchAsync(int id)
        {
            ItemPlanoClassificacao itemPlanoClassificacao = await SearchPersistenceAsync(id, true);

            ItemPlanoClassificacaoModel itemPlanoClassificacaoModel = _mapper.Map<ItemPlanoClassificacaoModel>(itemPlanoClassificacao);

            return itemPlanoClassificacaoModel;
        }

        public async Task<ICollection<ItemPlanoClassificacaoModel>> SearchByPlanoClassificacaoAsync(int idPlanoClassificacao, int page, int count)
        {
            int skip = (page - 1) * count;

            List<ItemPlanoClassificacao> itensPlanosClassificacao = await _set.Where(ipc => ipc.PlanoClassificacao.Id == idPlanoClassificacao)
                                                                              .Include(ipc => ipc.PlanoClassificacao)
                                                                              .Include(ipc => ipc.NivelClassificacao)
                                                                              .Include(ipc => ipc.ItemPlanoClassificacaoPai)
                                                                              .OrderBy(ipc => !ipc.IdItemPlanoClassificacaoPai.HasValue)
                                                                              .ThenBy(ipc => ipc.IdItemPlanoClassificacaoPai.Value)
                                                                              .ThenBy(pc => pc.Descricao)
                                                                              .Skip(skip)
                                                                              .Take(count)
                                                                              .ToListAsync();

            List<ItemPlanoClassificacaoModel> itensPlanosClassificacaoModel = _mapper.Map<List<ItemPlanoClassificacaoModel>>(itensPlanosClassificacao);

            return itensPlanosClassificacaoModel;
        }

        public async Task<int> CountByPlanoClassificacao(int idPlanoClassificacao)
        {
            var count = await _set.Where(ipc => ipc.PlanoClassificacao.Id == idPlanoClassificacao)
                                  .CountAsync();

            return count;
        }

        public void Update(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            ItemPlanoClassificacao itemPlanoClassificacao = _mapper.Map<ItemPlanoClassificacao>(itemPlanoClassificacaoModel);

            _set.Update(itemPlanoClassificacao);
        }

        public async Task RemoveAsync(int id)
        {
            ItemPlanoClassificacao itemPlanoClassificacao = await SearchPersistenceAsync(id);

            _set.Remove(itemPlanoClassificacao);
        }

        private async Task<ItemPlanoClassificacao> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<ItemPlanoClassificacao> queryable = _set.Where(ipc => ipc.Id == id);

            if (getRelationship)
                queryable = queryable.Include(ipc => ipc.PlanoClassificacao)
                                     .Include(ipc => ipc.NivelClassificacao)
                                     .Include(ipc => ipc.ItemPlanoClassificacaoPai);

            ItemPlanoClassificacao itemPlanoClassificacao = await queryable.SingleOrDefaultAsync();

            return itemPlanoClassificacao;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<ItemPlanoClassificacao> GetRelationship()
        {
            IQueryable<ItemPlanoClassificacao> queryable = _set.Include(ipc => ipc.PlanoClassificacao)
                                                               .Include(ipc => ipc.NivelClassificacao)
                                                               .Include(ipc => ipc.ItemPlanoClassificacaoPai);

            return queryable;
        }
    }
}
