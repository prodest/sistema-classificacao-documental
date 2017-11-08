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
    public class EFDocumentoRepository : IDocumentoRepository
    {
        private ScdContext _context;
        private DbSet<Documento> _set;

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFDocumentoRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.Documento;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<DocumentoModel> AddAsync(DocumentoModel documentoModel)
        {
            Documento documento = _mapper.Map<Documento>(documentoModel);

            var entityEntry = await _set.AddAsync(documento);

            await _unitOfWork.SaveAsync();

            documentoModel = _mapper.Map<DocumentoModel>(entityEntry.Entity);

            return documentoModel;
        }

        public async Task<DocumentoModel> SearchAsync(int id)
        {
            Documento documento = await SearchPersistenceAsync(id, true);

            DocumentoModel documentoModel = _mapper.Map<DocumentoModel>(documento);

            return documentoModel;
        }

        public async Task UpdateAsync(DocumentoModel documentoModel)
        {
            Documento documentoNew = _mapper.Map<Documento>(documentoModel);

            Documento documentoOld = await SearchPersistenceAsync(documentoNew.Id);

            _context.Entry(documentoOld).CurrentValues.SetValues(documentoNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            Documento documento = await SearchPersistenceAsync(id);

            _set.Remove(documento);

            await _unitOfWork.SaveAsync();
        }

        private async Task<Documento> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<Documento> queryable = _set.Where(ipc => ipc.Id == id);

            if (getRelationship)
                queryable = queryable.Include(ipc => ipc.ItemPlanoClassificacao)
                                     .Include(ipc => ipc.TipoDocumental);

            Documento documento = await queryable.SingleOrDefaultAsync();

            return documento;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<Documento> GetRelationship()
        {
            IQueryable<Documento> queryable = _set.Include(ipc => ipc.ItemPlanoClassificacao)
                                                  .Include(ipc => ipc.TipoDocumental)
                                                  ;

            return queryable;
        }
    }
}
