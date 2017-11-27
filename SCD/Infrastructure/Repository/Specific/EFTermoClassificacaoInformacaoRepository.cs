using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Prodest.Scd.Business.Model;
using Prodest.Scd.Business.Repository;
using Prodest.Scd.Business.Repository.Base;
using Prodest.Scd.Infrastructure.Mapping;
using Prodest.Scd.Persistence.Model;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Prodest.Scd.Infrastructure.Repository.Specific
{
    public class EFTermoClassificacaoInformacaoRepository : ITermoClassificacaoInformacaoRepository
    {
        private ScdContext _context;
        private DbSet<TermoClassificacaoInformacao> _set;

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFTermoClassificacaoInformacaoRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.TermoClassificacaoInformacao;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<TermoClassificacaoInformacaoModel> AddAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            TermoClassificacaoInformacao termoClassificacaoInformacao = _mapper.Map<TermoClassificacaoInformacao>(termoClassificacaoInformacaoModel);

            var entityEntry = await _set.AddAsync(termoClassificacaoInformacao);

            await _unitOfWork.SaveAsync();

            termoClassificacaoInformacaoModel = _mapper.Map<TermoClassificacaoInformacaoModel>(entityEntry.Entity);

            return termoClassificacaoInformacaoModel;
        }

        public async Task<TermoClassificacaoInformacaoModel> SearchAsync(int id)
        {
            TermoClassificacaoInformacao termoClassificacaoInformacao = await SearchPersistenceAsync(id, true);

            TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel = _mapper.Map<TermoClassificacaoInformacaoModel>(termoClassificacaoInformacao);

            return termoClassificacaoInformacaoModel;
        }

        public async Task<ICollection<TermoClassificacaoInformacaoModel>> SearchByUserAsync(string cpfUsuario)
        {
            ICollection<TermoClassificacaoInformacao> termosClassificacaoInformacao = await _set.Where(tci => tci.CpfUsuario.Equals(cpfUsuario))
                                                                                                .ToListAsync();

            ICollection<TermoClassificacaoInformacaoModel> termosClassificacaoInformacaoModel = _mapper.Map<ICollection<TermoClassificacaoInformacaoModel>>(termosClassificacaoInformacao);

            return termosClassificacaoInformacaoModel;
        }

        public async Task UpdateAsync(TermoClassificacaoInformacaoModel termoClassificacaoInformacaoModel)
        {
            TermoClassificacaoInformacao termoClassificacaoInformacaoNew = _mapper.Map<TermoClassificacaoInformacao>(termoClassificacaoInformacaoModel);

            TermoClassificacaoInformacao termoClassificacaoInformacaoOld = await SearchPersistenceAsync(termoClassificacaoInformacaoNew.Id);

            _context.Entry(termoClassificacaoInformacaoOld).CurrentValues.SetValues(termoClassificacaoInformacaoNew);

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            TermoClassificacaoInformacao termoClassificacaoInformacao = await SearchPersistenceAsync(id);

            _set.Remove(termoClassificacaoInformacao);

            await _unitOfWork.SaveAsync();
        }

        private async Task<TermoClassificacaoInformacao> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<TermoClassificacaoInformacao> queryable = _set.Where(ipc => ipc.Id == id);

            if (getRelationship)
                //queryable = queryable.Include(ipc => ipc.ItemPlanoClassificacao)
                                     //.Include(ipc => ipc.TipoDocumental)
                                     ;

            TermoClassificacaoInformacao termoClassificacaoInformacao = await queryable.SingleOrDefaultAsync();

            return termoClassificacaoInformacao;
        }

        //TODO: Verificar como utilizar este método
        private IQueryable<TermoClassificacaoInformacao> GetRelationship()
        {
            IQueryable<TermoClassificacaoInformacao> queryable = _set/*.Include(ipc => ipc.ItemPlanoClassificacao)*/
                                                  //.Include(ipc => ipc.TipoDocumental)
                                                  ;

            return queryable;
        }
    }
}
