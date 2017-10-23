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
    public class EFNivelClassificacaoRepository : INivelClassificacaoRepository
    {
        private ScdContext _context;
        private DbSet<NivelClassificacao> _set;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFNivelClassificacaoRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.NivelClassificacao;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<NivelClassificacaoModel> AddAsync(NivelClassificacaoModel nivelClassificacaoModel)
        {
            NivelClassificacao nivelClassificacao = _mapper.Map<NivelClassificacao>(nivelClassificacaoModel);

            EntityEntry<NivelClassificacao> entityEntry = await _set.AddAsync(nivelClassificacao);

            await _unitOfWork.SaveAsync();

            nivelClassificacaoModel = _mapper.Map<NivelClassificacaoModel>(entityEntry.Entity);

            return nivelClassificacaoModel;
        }

        public async Task<NivelClassificacaoModel> SearchAsync(int id)
        {
            NivelClassificacao nivelClassificacao = await SearchPersistenceAsync(id, true);

            NivelClassificacaoModel nivelClassificacaoModel = _mapper.Map<NivelClassificacaoModel>(nivelClassificacao);

            return nivelClassificacaoModel;
        }

        public async Task<ICollection<NivelClassificacaoModel>> SearchByOrganizacaoAsync(Guid guidOrganizacao, int page, int count)
        {
            int skip = (page - 1) * count;

            List<NivelClassificacao> niveisClassificacao = await _set.Where(nc => nc.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
                                                                     .Include(nc => nc.Organizacao)
                                                                     .OrderBy(nc => nc.Ativo)
                                                                     .ThenBy(nc => nc.Descricao)
                                                                     .Skip(skip)
                                                                     .Take(count)
                                                                     .ToListAsync();

            ICollection<NivelClassificacaoModel> niveisClassificacaoModel = _mapper.Map<ICollection<NivelClassificacaoModel>>(niveisClassificacao);

            return niveisClassificacaoModel;
        }

        public async Task<int> CountByOrganizacaoAsync(Guid guidOrganizacao)
        {
            var count = await _set.Where(nc => nc.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
                                  .CountAsync();

            return count;
        }

        public async Task UpdateAsync(NivelClassificacaoModel nivelClassificacaoModel)
        {
            NivelClassificacao nivelClassificacaoNew = _mapper.Map<NivelClassificacao>(nivelClassificacaoModel);

            NivelClassificacao nivelClassificacaoOld = await SearchPersistenceAsync(nivelClassificacaoNew.Id);

            _context.Entry(nivelClassificacaoOld).CurrentValues.SetValues(nivelClassificacaoNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            NivelClassificacao nivelClassificacao = await SearchPersistenceAsync(id);

            _set.Remove(nivelClassificacao);

            await _unitOfWork.SaveAsync();
        }

        private async Task<NivelClassificacao> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<NivelClassificacao> queryable = _set.Where(nc => nc.Id == id);

            if (getRelationship)
                queryable = queryable.Include(nc => nc.Organizacao);

            NivelClassificacao nivelClassificacao = await queryable.SingleOrDefaultAsync();

            return nivelClassificacao;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<NivelClassificacao> GetRelationship()
        {
            IQueryable<NivelClassificacao> queryable = _set.Include(nc => nc.Organizacao);

            return queryable;
        }
    }
}
