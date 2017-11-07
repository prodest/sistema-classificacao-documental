using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Infrastructure.Mapping;
using Prodest.Scd.Persistence.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Prodest.Scd.Infrastructure.Repository.Specific
{
    public class EFCriterioRestricaoRepository : ICriterioRestricaoRepository
    {
        private ScdContext _context;
        private DbSet<CriterioRestricao> _set;

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFCriterioRestricaoRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.CriterioRestricao;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CriterioRestricaoModel> AddAsync(CriterioRestricaoModel criterioRestricaoModel)
        {
            CriterioRestricao criterioRestricao = _mapper.Map<CriterioRestricao>(criterioRestricaoModel);

            var entityEntry = await _set.AddAsync(criterioRestricao);

            await _unitOfWork.SaveAsync();

            criterioRestricaoModel = _mapper.Map<CriterioRestricaoModel>(entityEntry.Entity);

            return criterioRestricaoModel;
        }

        public async Task<CriterioRestricaoModel> SearchAsync(int id)
        {
            CriterioRestricao criterioRestricao = await SearchPersistenceAsync(id, true);

            CriterioRestricaoModel criterioRestricaoModel = _mapper.Map<CriterioRestricaoModel>(criterioRestricao);

            return criterioRestricaoModel;
        }

        public async Task UpdateAsync(CriterioRestricaoModel criterioRestricaoModel)
        {
            CriterioRestricao criterioRestricaoNew = _mapper.Map<CriterioRestricao>(criterioRestricaoModel);

            CriterioRestricao criterioRestricaoOld = await SearchPersistenceAsync(criterioRestricaoNew.Id);

            _context.Entry(criterioRestricaoOld).CurrentValues.SetValues(criterioRestricaoNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            CriterioRestricao criterioRestricao = await SearchPersistenceAsync(id);

            _set.Remove(criterioRestricao);

            await _unitOfWork.SaveAsync();
        }

        private async Task<CriterioRestricao> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<CriterioRestricao> queryable = _set.Where(cr => cr.Id == id);

            //if (getRelationship)
            //    queryable = queryable.Include(cr => cr.);

            CriterioRestricao criterioRestricao = await queryable.SingleOrDefaultAsync();

            return criterioRestricao;
        }
    }
}