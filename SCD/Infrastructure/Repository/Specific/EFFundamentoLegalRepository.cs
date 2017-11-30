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
    public class EFFundamentoLegalRepository : IFundamentoLegalRepository
    {
        private ScdContext _context;
        private DbSet<FundamentoLegal> _set;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFFundamentoLegalRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.FundamentoLegal;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<FundamentoLegalModel> AddAsync(FundamentoLegalModel fundamentoLegalModel)
        {
            FundamentoLegal fundamentoLegal = _mapper.Map<FundamentoLegal>(fundamentoLegalModel);

            EntityEntry<FundamentoLegal> entityEntry = await _set.AddAsync(fundamentoLegal);

            await _unitOfWork.SaveAsync();

            fundamentoLegalModel = _mapper.Map<FundamentoLegalModel>(entityEntry.Entity);

            return fundamentoLegalModel;
        }

        public async Task<FundamentoLegalModel> SearchAsync(int id)
        {
            FundamentoLegal fundamentoLegal = await SearchPersistenceAsync(id, true);

            FundamentoLegalModel fundamentoLegalModel = _mapper.Map<FundamentoLegalModel>(fundamentoLegal);

            return fundamentoLegalModel;
        }

        public async Task<ICollection<FundamentoLegalModel>> SearchByOrganizacaoAsync(Guid guidOrganizacao, int page, int count)
        {
            int skip = (page - 1) * count;

            List<FundamentoLegal> fundamentosLegais = await _set.Where(nc => nc.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
                                                                     .Include(nc => nc.Organizacao)
                                                                     .OrderBy(nc => nc.Codigo)
                                                                     .ThenBy(nc => nc.Descricao)
                                                                     .Skip(skip)
                                                                     .Take(count)
                                                                     .ToListAsync();

            ICollection<FundamentoLegalModel> fundamentosLegaisModel = _mapper.Map<ICollection<FundamentoLegalModel>>(fundamentosLegais);

            return fundamentosLegaisModel;
        }

        public async Task<int> CountByOrganizacaoAsync(Guid guidOrganizacao)
        {
            var count = await _set.Where(nc => nc.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
                                  .CountAsync();

            return count;
        }

        public async Task UpdateAsync(FundamentoLegalModel fundamentoLegalModel)
        {
            FundamentoLegal fundamentoLegalNew = _mapper.Map<FundamentoLegal>(fundamentoLegalModel);

            FundamentoLegal fundamentoLegalOld = await SearchPersistenceAsync(fundamentoLegalNew.Id);

            _context.Entry(fundamentoLegalOld).CurrentValues.SetValues(fundamentoLegalNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            FundamentoLegal fundamentoLegal = await SearchPersistenceAsync(id);

            _set.Remove(fundamentoLegal);

            await _unitOfWork.SaveAsync();
        }

        private async Task<FundamentoLegal> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<FundamentoLegal> queryable = _set.Where(nc => nc.Id == id);

            if (getRelationship)
                queryable = queryable.Include(nc => nc.Organizacao);

            FundamentoLegal fundamentoLegal = await queryable.SingleOrDefaultAsync();

            return fundamentoLegal;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<FundamentoLegal> GetRelationship()
        {
            IQueryable<FundamentoLegal> queryable = _set.Include(nc => nc.Organizacao);

            return queryable;
        }
    }
}
