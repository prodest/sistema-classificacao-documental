using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Infrastructure.Mapping;
using Prodest.Scd.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Infrastructure.Repository.Specific
{
    public class EFPlanoClassificacaoRepository : IPlanoClassificacaoRepository
    {
        private ScdContext _context;
        private DbSet<PlanoClassificacao> _set;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFPlanoClassificacaoRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.PlanoClassificacao;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PlanoClassificacaoModel> AddAsync(PlanoClassificacaoModel planoClassificacaoModel)
        {
            PlanoClassificacao planoClassificacao = _mapper.Map<PlanoClassificacao>(planoClassificacaoModel);

            EntityEntry<PlanoClassificacao> entityEntry = await _set.AddAsync(planoClassificacao);

            await _unitOfWork.SaveAsync();

            planoClassificacaoModel = _mapper.Map<PlanoClassificacaoModel>(entityEntry.Entity);

            return planoClassificacaoModel;
        }

        public async Task<PlanoClassificacaoModel> SearchAsync(int id)
        {
            PlanoClassificacao planoClassificacao = await SearchPersistenceAsync(id, true);

            PlanoClassificacaoModel planoClassificacaoModel = _mapper.Map<PlanoClassificacaoModel>(planoClassificacao);

            return planoClassificacaoModel;
        }

        public async Task<PlanoClassificacaoModel> SearchByDocumentoAsync(int idDocumento)
        {
            PlanoClassificacao planoClassificacao = await _set.Where(pc => pc.ItensPlanoClassificacao.Any(ipc => ipc.Documentos.Any(d => d.Id == idDocumento)))
                                                              .SingleOrDefaultAsync();

            PlanoClassificacaoModel planoClassificacaoModel = _mapper.Map<PlanoClassificacaoModel>(planoClassificacao);

            return planoClassificacaoModel;
        }

        public async Task<PlanoClassificacaoModel> SearchCompleteAsync(int id)
        {
            PlanoClassificacao planoClassificacao = await SearchPersistenceAsync(id, true, true);

            PlanoClassificacaoModel planoClassificacaoModel = _mapper.Map<PlanoClassificacaoModel>(planoClassificacao);

            planoClassificacaoModel.ItensPlanoClassificacao = planoClassificacaoModel.ItensPlanoClassificacao
                                                                                        .Where(p => p.ItemPlanoClassificacaoParent == null)
                                                                                        .ToList();

            return planoClassificacaoModel;
        }

        public async Task<ICollection<PlanoClassificacaoModel>> SearchByOrganizacaoAsync(Guid guidOrganizacao, int page, int count)
        {
            int skip = (page - 1) * count;

            List<PlanoClassificacao> planosClassificacao = await _set.Where(pc => pc.GuidOrganizacao.Equals(guidOrganizacao))
                                                                     .Include(pc => pc.Organizacao)
                                                                     .OrderBy(pc => pc.InicioVigencia.HasValue)
                                                                     .ThenByDescending(pc => pc.InicioVigencia)
                                                                     .ThenByDescending(pc => pc.Codigo)
                                                                     .Skip(skip)
                                                                     .Take(count)
                                                                     .ToListAsync();

            ICollection<PlanoClassificacaoModel> planosClassificacaoModel = _mapper.Map<ICollection<PlanoClassificacaoModel>>(planosClassificacao);

            return planosClassificacaoModel;
        }

        public async Task<int> CountByOrganizacaoAsync(Guid guidOrganizacao)
        {
            var count = await _set.Where(pc => pc.GuidOrganizacao.Equals(guidOrganizacao))
                                  .CountAsync();

            return count;
        }

        public async Task UpdateAsync(PlanoClassificacaoModel planoClassificacaoModel)
        {
            PlanoClassificacao planoClassificacaoNew = _mapper.Map<PlanoClassificacao>(planoClassificacaoModel);

            PlanoClassificacao planoClassificacaoOld = await SearchPersistenceAsync(planoClassificacaoNew.Id);

            _context.Entry(planoClassificacaoOld).CurrentValues.SetValues(planoClassificacaoNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            PlanoClassificacao planoClassificacao = await SearchPersistenceAsync(id);

            _set.Remove(planoClassificacao);

            await _unitOfWork.SaveAsync();
        }

        private async Task<PlanoClassificacao> SearchPersistenceAsync(int id, bool getRelationship = false, bool getHierarchy = false)
        {
            IQueryable<PlanoClassificacao> queryable = _set.Where(pc => pc.Id == id);

            if (getRelationship)
                queryable = queryable.Include(pc => pc.Organizacao);

            if (getHierarchy)
            {
                queryable = queryable.Include(pc => pc.ItensPlanoClassificacao).ThenInclude(ipc => ipc.Documentos).ThenInclude(d => d.TipoDocumental)
                                     .Include(pc => pc.ItensPlanoClassificacao).ThenInclude(ipc => ipc.Documentos).ThenInclude(d => d.Sigilos)
                                     .Include(pc => pc.ItensPlanoClassificacao).ThenInclude(ipc => ipc.NivelClassificacao);
            }

            PlanoClassificacao planoClassificacao = await queryable.SingleOrDefaultAsync();

            return planoClassificacao;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<PlanoClassificacao> GetRelationship()
        {
            IQueryable<PlanoClassificacao> queryable = _set.Include(pc => pc.Organizacao);

            return queryable;
        }
    }
}
