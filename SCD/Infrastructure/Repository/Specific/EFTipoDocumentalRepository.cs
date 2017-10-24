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
    public class EFTipoDocumentalRepository : ITipoDocumentalRepository
    {
        private ScdContext _context;
        private DbSet<TipoDocumental> _set;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFTipoDocumentalRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.TipoDocumental;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TipoDocumentalModel> AddAsync(TipoDocumentalModel tipoDocumentalModel)
        {
            TipoDocumental tipoDocumental = _mapper.Map<TipoDocumental>(tipoDocumentalModel);

            EntityEntry<TipoDocumental> entityEntry = await _set.AddAsync(tipoDocumental);

            await _unitOfWork.SaveAsync();

            tipoDocumentalModel = _mapper.Map<TipoDocumentalModel>(entityEntry.Entity);

            return tipoDocumentalModel;
        }

        public async Task<TipoDocumentalModel> SearchAsync(int id)
        {
            TipoDocumental tipoDocumental = await SearchPersistenceAsync(id, true);

            TipoDocumentalModel tipoDocumentalModel = _mapper.Map<TipoDocumentalModel>(tipoDocumental);

            return tipoDocumentalModel;
        }

        public async Task<ICollection<TipoDocumentalModel>> SearchByOrganizacaoAsync(Guid guidOrganizacao, int page, int count)
        {
            int skip = (page - 1) * count;

            List<TipoDocumental> tiposDocumentais = await _set.Where(td => td.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
                                                              .Include(td => td.Organizacao)
                                                              .OrderBy(td => td.Ativo)
                                                              .ThenBy(td => td.Descricao)
                                                              .Skip(skip)
                                                              .Take(count)
                                                              .ToListAsync();

            ICollection<TipoDocumentalModel> tiposDocumentaisModel = _mapper.Map<ICollection<TipoDocumentalModel>>(tiposDocumentais);

            return tiposDocumentaisModel;
        }

        public async Task<int> CountByOrganizacaoAsync(Guid guidOrganizacao)
        {
            var count = await _set.Where(td => td.Organizacao.GuidOrganizacao.Equals(guidOrganizacao))
                                  .CountAsync();

            return count;
        }

        public async Task UpdateAsync(TipoDocumentalModel tipoDocumentalModel)
        {
            TipoDocumental tipoDocumentalNew = _mapper.Map<TipoDocumental>(tipoDocumentalModel);

            TipoDocumental tipoDocumentalOld = await SearchPersistenceAsync(tipoDocumentalNew.Id);

            _context.Entry(tipoDocumentalOld).CurrentValues.SetValues(tipoDocumentalNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            TipoDocumental tipoDocumental = await SearchPersistenceAsync(id);

            _set.Remove(tipoDocumental);

            await _unitOfWork.SaveAsync();
        }

        private async Task<TipoDocumental> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<TipoDocumental> queryable = _set.Where(td => td.Id == id);

            if (getRelationship)
                queryable = queryable.Include(nc => nc.Organizacao);

            TipoDocumental tipoDocumental = await queryable.SingleOrDefaultAsync();

            return tipoDocumental;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<TipoDocumental> GetRelationship()
        {
            IQueryable<TipoDocumental> queryable = _set.Include(td => td.Organizacao);

            return queryable;
        }
    }
}
