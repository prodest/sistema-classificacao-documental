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
    public class EFTemporalidadeRepository : ITemporalidadeRepository
    {
        private ScdContext _context;
        private DbSet<Temporalidade> _set;

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFTemporalidadeRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.Temporalidade;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TemporalidadeModel> AddAsync(TemporalidadeModel temporalidadeModel)
        {
            Temporalidade temporalidade = _mapper.Map<Temporalidade>(temporalidadeModel);

            var entityEntry = await _set.AddAsync(temporalidade);

            await _unitOfWork.SaveAsync();

            temporalidadeModel = _mapper.Map<TemporalidadeModel>(entityEntry.Entity);

            return temporalidadeModel;
        }

        public async Task<TemporalidadeModel> SearchAsync(int id)
        {
            Temporalidade temporalidade = await SearchPersistenceAsync(id, true);

            TemporalidadeModel temporalidadeModel = _mapper.Map<TemporalidadeModel>(temporalidade);

            return temporalidadeModel;
        }

        public async Task UpdateAsync(TemporalidadeModel temporalidadeModel)
        {
            Temporalidade temporalidadeNew = _mapper.Map<Temporalidade>(temporalidadeModel);

            Temporalidade temporalidadeOld = await SearchPersistenceAsync(temporalidadeNew.Id);

            _context.Entry(temporalidadeOld).CurrentValues.SetValues(temporalidadeNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            Temporalidade temporalidade = await SearchPersistenceAsync(id);

            _set.Remove(temporalidade);

            await _unitOfWork.SaveAsync();
        }

        private async Task<Temporalidade> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<Temporalidade> queryable = _set.Where(ipc => ipc.Id == id);

            if (getRelationship)
                queryable = queryable.Include(ipc => ipc.Documento);

            Temporalidade temporalidade = await queryable.SingleOrDefaultAsync();

            return temporalidade;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<Temporalidade> GetRelationship()
        {
            IQueryable<Temporalidade> queryable = _set.Include(ipc => ipc.Documento);

            return queryable;
        }
    }
}