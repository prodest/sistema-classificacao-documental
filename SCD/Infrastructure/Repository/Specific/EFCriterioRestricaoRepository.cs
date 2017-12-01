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
    public class EFCriterioRestricaoRepository : ICriterioRestricaoRepository
    {
        private ScdContext _context;
        private DbSet<CriterioRestricao> _set;
        private DbSet<CriterioRestricaoDocumento> _setCriterioRestricaoDocumento;

        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public EFCriterioRestricaoRepository(ScdContext context, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _context = context;
            _set = _context.CriterioRestricao;
            _setCriterioRestricaoDocumento = _context.CriterioRestricaoDocumento;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CriterioRestricaoModel> AddAsync(CriterioRestricaoModel criterioRestricaoModel)
        {
            CriterioRestricao criterioRestricao = _mapper.Map<CriterioRestricao>(criterioRestricaoModel);

            if (criterioRestricaoModel.Documentos != null && criterioRestricaoModel.Documentos.Count > 0)
            {
                criterioRestricao.CriteriosRestricaoDocumento = new List<CriterioRestricaoDocumento>();

                foreach (var documento in criterioRestricaoModel.Documentos)
                {
                    CriterioRestricaoDocumento criterioRestricaoDocumento = new CriterioRestricaoDocumento
                    {
                        IdDocumento = documento.Id
                    };

                    criterioRestricao.CriteriosRestricaoDocumento.Add(criterioRestricaoDocumento);
                }
            }

            var entityEntry = await _set.AddAsync(criterioRestricao);

            await _unitOfWork.SaveAsync();

            criterioRestricaoModel = _mapper.Map<CriterioRestricaoModel>(entityEntry.Entity);

            criterioRestricaoModel.Documentos = _mapper.Map<ICollection<DocumentoModel>>(criterioRestricao.CriteriosRestricaoDocumento.Select(crd => crd.Documento).ToList());

            return criterioRestricaoModel;
        }

        public async Task<CriterioRestricaoModel> SearchAsync(int id)
        {
            CriterioRestricao criterioRestricao = await SearchPersistenceAsync(id, true);

            CriterioRestricaoModel criterioRestricaoModel = _mapper.Map<CriterioRestricaoModel>(criterioRestricao);

            //TODO: Verificar uma forma melhor de fazer isso
            if (criterioRestricaoModel != null)
                criterioRestricaoModel.Documentos = _mapper.Map<ICollection<DocumentoModel>>(criterioRestricao.CriteriosRestricaoDocumento.Select(crd => crd.Documento).ToList());

            return criterioRestricaoModel;
        }

        public async Task<ICollection<CriterioRestricaoModel>> SearchByPlanoClassificacaoAsync(int idPlanoClassificacao)
        {
            ICollection<CriterioRestricao> criteriosRestricao = await _set.Where(p => p.IdPlanoClassificacao == idPlanoClassificacao).ToListAsync();

            ICollection<CriterioRestricaoModel> criteriosRestricaoModel = _mapper.Map<ICollection<CriterioRestricaoModel>>(criteriosRestricao);

            return criteriosRestricaoModel;
        }

        public async Task UpdateAsync(CriterioRestricaoModel criterioRestricaoModel)
        {
            CriterioRestricao criterioRestricaoNew = _mapper.Map<CriterioRestricao>(criterioRestricaoModel);

            CriterioRestricao criterioRestricaoOld = await SearchPersistenceWithDocumentosAsync(criterioRestricaoNew.Id);

            _context.Entry(criterioRestricaoOld).CurrentValues.SetValues(criterioRestricaoNew);

            List<int> idsRemove = criterioRestricaoOld.CriteriosRestricaoDocumento.Select(crd => crd.Id).ToList();

            //Remover as associações que não existem mais
            foreach (var criterioRestricaoDocumentoID in idsRemove)
            {
                var documento = criterioRestricaoModel.Documentos.Where(d => d.Id == criterioRestricaoDocumentoID)
                                                               .SingleOrDefault();

                bool exists = documento != null ? true : false;
                //TODO: Não é possível remover assim, pois dá pau na iteração
                if (!exists)
                {
                    var criterioRestricaoDocumento = criterioRestricaoOld.CriteriosRestricaoDocumento.Single(crd => crd.Id == criterioRestricaoDocumentoID);
                    criterioRestricaoOld.CriteriosRestricaoDocumento.Remove(criterioRestricaoDocumento);
                    _setCriterioRestricaoDocumento.Remove(criterioRestricaoDocumento);
                }
            }

            //Adicionar as associações que ainda não existem
            foreach (var documento in criterioRestricaoModel.Documentos)
            {
                var criterioRestricaoDocumento = criterioRestricaoOld.CriteriosRestricaoDocumento.Where(crd => crd.IdDocumento == documento.Id)
                                                                                                 .SingleOrDefault();

                bool exists = criterioRestricaoDocumento != null ? true : false;

                if (!exists)
                {
                    criterioRestricaoOld.CriteriosRestricaoDocumento.Add(new CriterioRestricaoDocumento
                    {
                        IdCriterioRestricao = criterioRestricaoModel.Id,
                        IdDocumento = documento.Id
                    });
                }
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task RemoveAsync(int id)
        {
            CriterioRestricao criterioRestricao = await SearchPersistenceWithDocumentosAsync(id);

            if (criterioRestricao.CriteriosRestricaoDocumento.Count > 0)
                _setCriterioRestricaoDocumento.RemoveRange(criterioRestricao.CriteriosRestricaoDocumento);

            _set.Remove(criterioRestricao);

            await _unitOfWork.SaveAsync();
        }

        private async Task<CriterioRestricao> SearchPersistenceAsync(int id, bool getRelationship = false)
        {
            IQueryable<CriterioRestricao> queryable = _set.Where(cr => cr.Id == id);

            if (getRelationship)
                queryable = queryable.Include(cr => cr.PlanoClassificacao)
                                                            .Include(cr => cr.FundamentoLegal)
                                     .Include(cr => cr.CriteriosRestricaoDocumento).ThenInclude(crd => crd.Documento);

            CriterioRestricao criterioRestricao = await queryable.SingleOrDefaultAsync();

            return criterioRestricao;
        }

        private async Task<CriterioRestricao> SearchPersistenceWithDocumentosAsync(int id)
        {
            IQueryable<CriterioRestricao> queryable = _set.Where(cr => cr.Id == id)
                                                          .Include(cr => cr.PlanoClassificacao)
                                                          .Include(cr => cr.CriteriosRestricaoDocumento).ThenInclude(crd => crd.Documento);

            CriterioRestricao criterioRestricao = await queryable.SingleOrDefaultAsync();

            return criterioRestricao;
        }
    }
}