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
    public class EFSigiloRepository : ISigiloRepository
    {
        private ScdContext _context;
        private DbSet<Sigilo> _set;

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFSigiloRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.Sigilo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<SigiloModel> AddAsync(SigiloModel sigiloModel)
        {
            Sigilo sigilo = _mapper.Map<Sigilo>(sigiloModel);

            var entityEntry = await _set.AddAsync(sigilo);

            await _unitOfWork.SaveAsync();

            sigiloModel = _mapper.Map<SigiloModel>(entityEntry.Entity);

            return sigiloModel;
        }

        public async Task<SigiloModel> SearchAsync(int id)
        {
            Sigilo sigilo = await SearchPersistenceAsync(id, true);

            SigiloModel sigiloModel = _mapper.Map<SigiloModel>(sigilo);

            return sigiloModel;
        }

        public async Task UpdateAsync(SigiloModel sigiloModel)
        {
            Sigilo sigiloNew = _mapper.Map<Sigilo>(sigiloModel);

            Sigilo sigiloOld = await SearchPersistenceAsync(sigiloNew.Id);

            _context.Entry(sigiloOld).CurrentValues.SetValues(sigiloNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            Sigilo sigilo = await SearchPersistenceAsync(id);

            _set.Remove(sigilo);

            await _unitOfWork.SaveAsync();
        }

        private async Task<Sigilo> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<Sigilo> queryable = _set.Where(ipc => ipc.Id == id);

            if (getRelationship)
                queryable = queryable.Include(ipc => ipc.Documento);

            Sigilo sigilo = await queryable.SingleOrDefaultAsync();

            return sigilo;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<Sigilo> GetRelationship()
        {
            IQueryable<Sigilo> queryable = _set.Include(ipc => ipc.Documento);

            return queryable;
        }
    }
}