using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Infrastructure.Mapping;
using Prodest.Scd.Persistence.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Infrastructure.Repository.Specific
{
    public class EFItemPlanoClassificacaoRepository : IItemPlanoClassificacaoRepository
    {
        private ScdContext _context;
        private DbSet<ItemPlanoClassificacao> _set;

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFItemPlanoClassificacaoRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.ItemPlanoClassificacao;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ItemPlanoClassificacaoModel> AddAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            ItemPlanoClassificacao itemPlanoClassificacao = _mapper.Map<ItemPlanoClassificacao>(itemPlanoClassificacaoModel);

            var entityEntry = await _set.AddAsync(itemPlanoClassificacao);

            await _unitOfWork.SaveAsync();

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
                                                                              .Include(ipc => ipc.ItemPlanoClassificacaoParent)
                                                                              .OrderBy(ipc => !ipc.IdItemPlanoClassificacaoParent.HasValue)
                                                                              .ThenBy(ipc => ipc.IdItemPlanoClassificacaoParent.Value)
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

        public async Task UpdateAsync(ItemPlanoClassificacaoModel itemPlanoClassificacaoModel)
        {
            ItemPlanoClassificacao itemPlanoClassificacaoNew = _mapper.Map<ItemPlanoClassificacao>(itemPlanoClassificacaoModel);

            ItemPlanoClassificacao itemPlanoClassificacaoOld = await SearchPersistenceAsync(itemPlanoClassificacaoNew.Id);

            _context.Entry(itemPlanoClassificacaoOld).CurrentValues.SetValues(itemPlanoClassificacaoNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            ItemPlanoClassificacao itemPlanoClassificacao = await SearchPersistenceAsync(id);

            _set.Remove(itemPlanoClassificacao);

            await _unitOfWork.SaveAsync();
        }

        public async Task<int> CountChildren(int id)
        {
            int count = await _set.Where(ipc => ipc.IdItemPlanoClassificacaoParent.HasValue
                                             && ipc.IdItemPlanoClassificacaoParent.Value == id)
                                  .CountAsync();

            return count;
        }

        private async Task<ItemPlanoClassificacao> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<ItemPlanoClassificacao> queryable = _set.Where(ipc => ipc.Id == id);

            if (getRelationship)
                queryable = queryable.Include(ipc => ipc.PlanoClassificacao)
                                     .Include(ipc => ipc.NivelClassificacao)
                                     .Include(ipc => ipc.ItemPlanoClassificacaoParent);

            ItemPlanoClassificacao itemPlanoClassificacao = await queryable.SingleOrDefaultAsync();

            return itemPlanoClassificacao;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<ItemPlanoClassificacao> GetRelationship()
        {
            IQueryable<ItemPlanoClassificacao> queryable = _set.Include(ipc => ipc.PlanoClassificacao)
                                                               .Include(ipc => ipc.NivelClassificacao)
                                                               .Include(ipc => ipc.ItemPlanoClassificacaoParent);

            return queryable;
        }
    }
}
